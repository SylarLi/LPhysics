using System;
using System.Runtime.Serialization;

internal class LUDecomposition
{
	#region Class variables
		
	/// <summary>Array for internal storage of decomposition.
	/// @serial internal array storage.
	/// </summary>
	private LFloat[][] LU;
		
	/// <summary>Row and column dimensions, and pivot sign.
	/// @serial column dimension.
	/// @serial row dimension.
	/// @serial pivot sign.
	/// </summary>
	private int m, n, pivsign;
		
	/// <summary>Internal storage of pivot vector.
	/// @serial pivot vector.
	/// </summary>
	private int[] piv;
		
	#endregion //  Class variables

	#region Constructor
		
	/// <summary>LU Decomposition</summary>
	/// <param name="A">  Rectangular matrix
	/// </param>
	/// <returns>     Structure to access L, U and piv.
	/// </returns>
		
	public LUDecomposition(LMatrix A)
	{
		// Use a "left-looking", dot-product, Crout/Doolittle algorithm.
			
		LU = A.ArrayCopy;
		m = A.Rows;
		n = A.Columns;
		piv = new int[m];
		for (int i = 0; i < m; i++)
		{
			piv[i] = i;
		}
		pivsign = 1;
		LFloat[] LUrowi;
        LFloat[] LUcolj = new LFloat[m];
			
		// Outer loop.
			
		for (int j = 0; j < n; j++)
		{
				
			// Make a copy of the j-th column to localize references.
				
			for (int i = 0; i < m; i++)
			{
				LUcolj[i] = LU[i][j];
			}
				
			// Apply previous transformations.
				
			for (int i = 0; i < m; i++)
			{
				LUrowi = LU[i];
					
				// Most of the time is spent in the following dot product.
					
				int kmax = System.Math.Min(i, j);
				LFloat s = 0;
				for (int k = 0; k < kmax; k++)
				{
					s += LUrowi[k] * LUcolj[k];
				}
					
				LUrowi[j] = LUcolj[i] -= s;
			}
				
			// Find pivot and exchange if necessary.
				
			int p = j;
			for (int i = j + 1; i < m; i++)
			{
				if (System.Math.Abs(LUcolj[i]) > System.Math.Abs(LUcolj[p]))
				{
					p = i;
				}
			}
			if (p != j)
			{
				for (int k = 0; k < n; k++)
				{
                    LFloat t = LU[p][k]; 
                    LU[p][k] = LU[j][k]; 
                    LU[j][k] = t;
				}
				int k2 = piv[p]; piv[p] = piv[j]; piv[j] = k2;
				pivsign = - pivsign;
			}
				
			// Compute multipliers.
				
			if (j < m & LU[j][j] != 0.0)
			{
				for (int i = j + 1; i < m; i++)
				{
					LU[i][j] /= LU[j][j];
				}
			}
		}
	}

	#endregion //  Constructor
				
	#region Public Properties

	/// <summary>Is the matrix nonsingular?</summary>
	/// <returns>     true if U, and hence A, is nonsingular.
	/// </returns>
	virtual public bool IsNonSingular
	{
		get
		{
			for (int j = 0; j < n; j++)
			{
				if (LU[j][j] == 0)
					return false;
			}
			return true;
		}
	}
				
	/// <summary>Solve A*X = B</summary>
	/// <param name="B">  A Matrix with as many rows as A and any number of columns.
	/// </param>
	/// <returns>     X so that L*U*X = B(piv,:)
	/// </returns>
	/// <exception cref="System.ArgumentException"> Matrix row dimensions must agree.
	/// </exception>
	/// <exception cref="System.SystemException"> Matrix is singular.
	/// </exception>

    public virtual LMatrix Solve(LMatrix B)
	{
		if (B.Rows != m)
		{
			throw new System.ArgumentException("Matrix row dimensions must agree.");
		}
		if (!this.IsNonSingular)
		{
			throw new System.SystemException("Matrix is singular.");
		}
			
		// Copy right hand side with pivoting
		int nx = B.Columns;
		LMatrix Xmat = B.GetMatrix(piv, 0, nx - 1);
		LFloat[][] X = Xmat.Array;
			
		// Solve L*Y = B(piv,:)
		for (int k = 0; k < n; k++)
		{
			for (int i = k + 1; i < n; i++)
			{
				for (int j = 0; j < nx; j++)
				{
					X[i][j] -= X[k][j] * LU[i][k];
				}
			}
		}
		// Solve U*X = Y;
		for (int k = n - 1; k >= 0; k--)
		{
			for (int j = 0; j < nx; j++)
			{
				X[k][j] /= LU[k][k];
			}
			for (int i = 0; i < k; i++)
			{
				for (int j = 0; j < nx; j++)
				{
					X[i][j] -= X[k][j] * LU[i][k];
				}
			}
		}
		return Xmat;
	}

	#endregion //  Public Methods
}