/// <summary>
/// Modified from GenernalMatrix of DotNetMatrix
/// </summary>
public class LMatrix
{
    private LFloat[][] A;

    private int m, n;
		
	public LMatrix(int m, int n)
	{
		this.m = m;
		this.n = n;
		A = new LFloat[m][];
		for (int i = 0; i < m; i++)
		{
			A[i] = new LFloat[n];
		}
	}
		
	public LMatrix(int m, int n, LFloat s)
	{
		this.m = m;
		this.n = n;
		A = new LFloat[m][];
		for (int i = 0; i < m; i++)
		{
			A[i] = new LFloat[n];
		}
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				A[i][j] = s;
			}
		}
	}
		
	public LMatrix(LFloat[][] A)
	{
		m = A.Length;
		n = A[0].Length;
		for (int i = 0; i < m; i++)
		{
			if (A[i].Length != n)
			{
				throw new System.ArgumentException("All rows must have the same length.");
			}
		}
		this.A = A;
	}
		
	public LMatrix(LFloat[][] A, int m, int n)
	{
		this.A = A;
		this.m = m;
		this.n = n;
	}
		
	public LMatrix(LFloat[] vals, int m)
	{
		this.m = m;
		n = (m != 0 ? vals.Length / m : 0);
		if (m * n != vals.Length)
		{
			throw new System.ArgumentException("Array length must be a multiple of m.");
		}
        A = new LFloat[m][];
		for (int i = 0; i < m; i++)
		{
            A[i] = new LFloat[n];
		}
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < n; j++)
			{
				A[i][j] = vals[i * n + j];
			}
		}
	}

    public override string ToString()
    {
        string print = "";
        for (int i = 0; i < m; i++)
        {
            print += "[";
            for (int j = 0; j < n; j++)
            {
                print += A[i][j];
                if (j != n - 1)
                {
                    print += ", ";
                }
            }
            print += "]";
            if (i != m - 1)
            {
                print += " ";
            }
        }
        return print;
    }

    public LFloat[][] Array
    {
        get
        {
            return A;
        }
    }

    public LFloat[][] ArrayCopy
    {
        get
        {
            LFloat[][] C = new LFloat[m][];
            for (int i = 0; i < m; i++)
            {
                C[i] = new LFloat[n];
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i][j] = A[i][j];
                }
            }
            return C;
        }
    }

    public int Rows
    {
        get
        {
            return m;
        }
    }

    public int Columns
    {
        get
        {
            return n;
        }
    }

    public LFloat[] this[int i]
    {
        get
        {
            return A[i];
        }
    }

    /// <summary>Get a submatrix.</summary>
    /// <param name="i0">  Initial row index
    /// </param>
    /// <param name="i1">  Final row index
    /// </param>
    /// <param name="j0">  Initial column index
    /// </param>
    /// <param name="j1">  Final column index
    /// </param>
    /// <returns>     A(i0:i1,j0:j1)
    /// </returns>
    /// <exception cref="System.IndexOutOfRangeException">   Submatrix indices
    /// </exception>
    public virtual LMatrix GetMatrix(int i0, int i1, int j0, int j1)
    {
        LMatrix X = new LMatrix(i1 - i0 + 1, j1 - j0 + 1);
        LFloat[][] B = X.Array;
        try
        {
            for (int i = i0; i <= i1; i++)
            {
                for (int j = j0; j <= j1; j++)
                {
                    B[i - i0][j - j0] = A[i][j];
                }
            }
        }
        catch (System.IndexOutOfRangeException e)
        {
            throw new System.IndexOutOfRangeException("Submatrix indices", e);
        }
        return X;
    }

    /// <summary>Get a submatrix.</summary>
    /// <param name="r">   Array of row indices.
    /// </param>
    /// <param name="c">   Array of column indices.
    /// </param>
    /// <returns>     A(r(:),c(:))
    /// </returns>
    /// <exception cref="System.IndexOutOfRangeException">   Submatrix indices
    /// </exception>

    public LMatrix GetMatrix(int[] r, int[] c)
    {
        LMatrix X = new LMatrix(r.Length, c.Length);
        LFloat[][] B = X.Array;
        try
        {
            for (int i = 0; i < r.Length; i++)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    B[i][j] = A[r[i]][c[j]];
                }
            }
        }
        catch (System.IndexOutOfRangeException e)
        {
            throw new System.IndexOutOfRangeException("Submatrix indices", e);
        }
        return X;
    }

    /// <summary>Get a submatrix.</summary>
    /// <param name="i0">  Initial row index
    /// </param>
    /// <param name="i1">  Final row index
    /// </param>
    /// <param name="c">   Array of column indices.
    /// </param>
    /// <returns>     A(i0:i1,c(:))
    /// </returns>
    /// <exception cref="System.IndexOutOfRangeException">   Submatrix indices
    /// </exception>

    public virtual LMatrix GetMatrix(int i0, int i1, int[] c)
    {
        LMatrix X = new LMatrix(i1 - i0 + 1, c.Length);
        LFloat[][] B = X.Array;
        try
        {
            for (int i = i0; i <= i1; i++)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    B[i - i0][j] = A[i][c[j]];
                }
            }
        }
        catch (System.IndexOutOfRangeException e)
        {
            throw new System.IndexOutOfRangeException("Submatrix indices", e);
        }
        return X;
    }

    /// <summary>Get a submatrix.</summary>
    /// <param name="r">   Array of row indices.
    /// </param>
    /// <param name="j0">  Initial column index
    /// </param>
    /// <param name="j1">  Final column index
    /// </param>
    /// <returns>     A(r(:),j0:j1)
    /// </returns>
    /// <exception cref="System.IndexOutOfRangeException">   Submatrix indices
    /// </exception>

    public virtual LMatrix GetMatrix(int[] r, int j0, int j1)
    {
        LMatrix X = new LMatrix(r.Length, j1 - j0 + 1);
        LFloat[][] B = X.Array;
        try
        {
            for (int i = 0; i < r.Length; i++)
            {
                for (int j = j0; j <= j1; j++)
                {
                    B[i][j - j0] = A[r[i]][j];
                }
            }
        }
        catch (System.IndexOutOfRangeException e)
        {
            throw new System.IndexOutOfRangeException("Submatrix indices", e);
        }
        return X;
    }

    /// <summary>C = A + B</summary>
    /// <param name="B">   another matrix
    /// </param>
    /// <returns>     A + B
    /// </returns>

    public LMatrix Add(LMatrix B)
    {
        CheckMatrixDimensions(B);
        LMatrix X = new LMatrix(m, n);
        LFloat[][] C = X.Array;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                C[i][j] = A[i][j] + B.A[i][j];
            }
        }
        return X;
    }

    /// <summary>C = A - B</summary>
    /// <param name="B">   another matrix
    /// </param>
    /// <returns>     A - B
    /// </returns>

    public virtual LMatrix Subtract(LMatrix B)
    {
        CheckMatrixDimensions(B);
        LMatrix X = new LMatrix(m, n);
        LFloat[][] C = X.Array;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                C[i][j] = A[i][j] - B.A[i][j];
            }
        }
        return X;
    }

    /// <summary>Multiply a matrix by a scalar, C = s*A</summary>
    /// <param name="s">   scalar
    /// </param>
    /// <returns>     s*A
    /// </returns>

    public LMatrix Multiply(LFloat s)
    {
        LMatrix X = new LMatrix(m, n);
        LFloat[][] C = X.Array;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                C[i][j] = s * A[i][j];
            }
        }
        return X;
    }

    /// <summary>Linear algebraic matrix multiplication, A * B</summary>
    /// <param name="B">   another matrix
    /// </param>
    /// <returns>     Matrix product, A * B
    /// </returns>
    /// <exception cref="System.ArgumentException">  Matrix inner dimensions must agree.
    /// </exception>

    public LMatrix Multiply(LMatrix B)
    {
        if (B.m != n)
        {
            throw new System.ArgumentException("GeneralMatrix inner dimensions must agree.");
        }
        LMatrix X = new LMatrix(m, B.n);
        LFloat[][] C = X.Array;
        LFloat[] Bcolj = new LFloat[n];
        for (int j = 0; j < B.n; j++)
        {
            for (int k = 0; k < n; k++)
            {
                Bcolj[k] = B.A[k][j];
            }
            for (int i = 0; i < m; i++)
            {
                LFloat[] Arowi = A[i];
                LFloat s = 0;
                for (int k = 0; k < n; k++)
                {
                    s += Arowi[k] * Bcolj[k];
                }
                C[i][j] = s;
            }
        }
        return X;
    }

    public LMatrix Transpose()
    {
        LMatrix X = new LMatrix(n, m);
        LFloat[][] C = X.Array;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                C[j][i] = A[i][j];
            }
        }
        return X;
    }

    public LMatrix Inverse()
    {
        return Solve(Identity(m, m));
    }

    public LMatrix Solve(LMatrix B)
    {
        return (m == n ? (new LUDecomposition(this)).Solve(B) : (new QRDecomposition(this)).Solve(B));
    }

    /// <summary>
    /// Check if size(A) == size(B)
    /// </summary>
    /// <param name="B"></param>
    private void CheckMatrixDimensions(LMatrix B)
    {
        if (B.m != m || B.n != n)
        {
            throw new System.ArgumentException("GeneralMatrix dimensions must agree.");
        }
    }

    public static LMatrix Identity(int m, int n)
    {
        LMatrix A = new LMatrix(m, n);
        LFloat[][] X = A.Array;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                X[i][j] = (i == j ? 1 : 0);
            }
        }
        return A;
    }

    /// <summary>
    ///  Addition of matrices
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    /// <returns></returns>
    public static LMatrix operator +(LMatrix m1, LMatrix m2)
    {
        return m1.Add(m2);
    }

    /// <summary>
    /// Subtraction of matrices
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    /// <returns></returns>
    public static LMatrix operator -(LMatrix m1, LMatrix m2)
    {
        return m1.Subtract(m2);
    }

    /// <summary>
    /// Multiplication of matrices
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    /// <returns></returns>
    public static LMatrix operator *(LMatrix m1, LMatrix m2)
    {
        return m1.Multiply(m2);
    }

    public static LMatrix operator *(LMatrix m, LFloat f)
    {
        return m.Multiply(f);
    }
}
