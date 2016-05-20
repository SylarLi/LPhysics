using System;
using System.Runtime.Serialization;

/// <summary>QR Decomposition.
/// For an m-by-n matrix A with m >= n, the QR decomposition is an m-by-n
/// orthogonal matrix Q and an n-by-n upper triangular matrix R so that
/// A = Q*R.
/// 
/// The QR decompostion always exists, even if the matrix does not have
/// full rank, so the constructor will never fail.  The primary use of the
/// QR decomposition is in the least squares solution of nonsquare systems
/// of simultaneous linear equations.  This will fail if IsFullRank()
/// returns false.
/// </summary>
	
internal class QRDecomposition
{
	#region Class variables
		
	/// <summary>Array for internal storage of decomposition.
	/// @serial internal array storage.
	/// </summary>
	private LFloat[][] QR;
		
	/// <summary>Row and column dimensions.
	/// @serial column dimension.
	/// @serial row dimension.
	/// </summary>
	private int m, n;
		
	/// <summary>Array for internal storage of diagonal of R.
	/// @serial diagonal of R.
	/// </summary>
	private LFloat[] Rdiag;

	#endregion //  Class variables
		
	#region Constructor
		
	/// <summary>QR Decomposition, computed by Householder reflections.</summary>
	/// <param name="A">   Rectangular matrix
	/// </param>
	/// <returns>     Structure to access R and the Householder vectors and compute Q.
	/// </returns>
		
	public QRDecomposition(LMatrix A)
	{
		// Initialize.
		QR = A.ArrayCopy;
		m = A.Rows;
		n = A.Columns;
		Rdiag = new LFloat[n];
			
		// Main loop.
		for (int k = 0; k < n; k++)
		{
			// Compute 2-norm of k-th column without under/overflow.
			LFloat nrm = 0;
			for (int i = k; i < m; i++)
			{
				nrm = LMatrixUtil.Hypot(nrm, QR[i][k]);
			}
				
			if (nrm != 0)
			{
				// Form k-th Householder vector.
				if (QR[k][k] < 0)
				{
					nrm = - nrm;
				}
				for (int i = k; i < m; i++)
				{
					QR[i][k] /= nrm;
				}
				QR[k][k] += 1;
					
				// Apply transformation to remaining columns.
				for (int j = k + 1; j < n; j++)
				{
					LFloat s = 0;
					for (int i = k; i < m; i++)
					{
						s += QR[i][k] * QR[i][j];
					}
					s = (- s) / QR[k][k];
					for (int i = k; i < m; i++)
					{
						QR[i][j] += s * QR[i][k];
					}
				}
			}
			Rdiag[k] = - nrm;
		}
	}

	#endregion //  Constructor
		
	#region Public Properties

	/// <summary>Is the matrix full rank?</summary>
	/// <returns>     true if R, and hence A, has full rank.
	/// </returns>
	virtual public bool FullRank
	{
		get
		{
			for (int j = 0; j < n; j++)
			{
				if (Rdiag[j] == 0)
					return false;
			}
			return true;
		}
	}

	#endregion //  Public Properties
		
	#region Public Methods
		
	/// <summary>Least squares solution of A*X = B</summary>
	/// <param name="B">   A Matrix with as many rows as A and any number of columns.
	/// </param>
	/// <returns>     X that minimizes the two norm of Q*R*X-B.
	/// </returns>
	/// <exception cref="System.ArgumentException"> Matrix row dimensions must agree.
	/// </exception>
	/// <exception cref="System.SystemException"> Matrix is rank deficient.
	/// </exception>

    public virtual LMatrix Solve(LMatrix B)
	{
		if (B.Rows != m)
		{
			throw new System.ArgumentException("GeneralMatrix row dimensions must agree.");
		}
		if (!this.FullRank)
		{
			throw new System.SystemException("Matrix is rank deficient.");
		}
			
		// Copy right hand side
		int nx = B.Columns;
		LFloat[][] X = B.ArrayCopy;
			
		// Compute Y = transpose(Q)*B
		for (int k = 0; k < n; k++)
		{
			for (int j = 0; j < nx; j++)
			{
				LFloat s = 0;
				for (int i = k; i < m; i++)
				{
					s += QR[i][k] * X[i][j];
				}
				s = (- s) / QR[k][k];
				for (int i = k; i < m; i++)
				{
					X[i][j] += s * QR[i][k];
				}
			}
		}
		// Solve R*X = Y;
		for (int k = n - 1; k >= 0; k--)
		{
			for (int j = 0; j < nx; j++)
			{
				X[k][j] /= Rdiag[k];
			}
			for (int i = 0; i < k; i++)
			{
				for (int j = 0; j < nx; j++)
				{
					X[i][j] -= X[k][j] * QR[i][k];
				}
			}
		}

        return (new LMatrix(X, n, nx).GetMatrix(0, n - 1, 0, nx - 1));
	}

	#endregion //  Public Methods
}