using Acidmanic.Mathematics.Exceptions;

namespace Acidmanic.Mathematics;

public partial class Matrix
{
    public static Matrix operator *(Matrix m1, Matrix m)
    {
        return PerformElementWise(m1, m, (d1, d2) => d1 * d2);
    }


    public static Matrix operator /(Matrix m1, Matrix m2)
    {
        return PerformElementWise(m1, m2, (d1, d2) => d1 / d2);
    }


    public static Matrix operator %(Matrix m1, Matrix m2)
    {
        return PerformElementWise(m1, m2, (d1, d2) => d1 % d2);
    }

    public static Matrix operator +(Matrix m1, Matrix m2)
    {
        return PerformElementWise(m1, m2, (d1, d2) => d1 + d2);
    }

    public static Matrix operator -(Matrix m1, Matrix m2)
    {
        return PerformElementWise(m1, m2, (d1, d2) => d1 - d2);
    }

    public static Matrix operator ^(Matrix m1, Matrix m2)
    {
        return PerformElementWise(m1, m2, Math.Pow);
    }

    public static Matrix operator ==(Matrix m1, Matrix m)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return PerformElementWise(m1, m, (d1, d2) => (d1 == d2) ? 1 : 0);
    }

    public static Matrix operator !=(Matrix m1, Matrix m2)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return PerformElementWise(m1, m2, (d1, d2) => (d1 == d2) ? 0 : 1);
    }


    public static Matrix PerformElementWise(Matrix m1, Matrix m, Func<double, double, double> calculate)
    {
        if (m1.Elements.Length != m.Elements.Length)
        {
            throw new InvalidMatrixSizeException();
        }

        var result = new Matrix(m1._indexMap.Size);

        for (int i = 0; i < result.Elements.Length; i++)
        {
            result.Elements[i] = calculate(m1.Elements[i], m.Elements[i]);
        }

        return result;
    }

    public static Matrix PerformElementWise(double d1, Matrix m, Func<double, double, double> calculate)
    {
        var result = new Matrix(m._indexMap.Size);

        for (int i = 0; i < result.Elements.Length; i++)
        {
            result.Elements[i] = calculate(d1, m.Elements[i]);
        }

        return result;
    }

    public static Matrix PerformElementWise(Matrix m, double d2, Func<double, double, double> calculate)
    {
        var result = new Matrix(m._indexMap.Size);

        for (int i = 0; i < result.Elements.Length; i++)
        {
            result.Elements[i] = calculate(m.Elements[i], d2);
        }

        return result;
    }

    public static Matrix PerformElementWise(Matrix m, Func<double, double> manipulate)
    {
        var result = new Matrix(m._indexMap.Size);

        for (int i = 0; i < result.Elements.Length; i++)
        {
            result.Elements[i] = manipulate(m.Elements[i]);
        }

        return result;
    }

    public static void ManipulateElementWise(Matrix m, Func<double, double> manipulate)
    {
        for (int i = 0; i < m.Elements.Length; i++)
        {
            m.Elements[i] = manipulate(m.Elements[i]);
        }
    }

    public static Matrix operator *(double d, Matrix m)
    {
        return PerformElementWise(d, m, (d1, d2) => d1 * d2);
    }

    public static Matrix operator /(double d, Matrix m)
    {
        return PerformElementWise(d, m, (d1, d2) => d1 / d2);
    }

    public static Matrix operator %(double d, Matrix m)
    {
        return PerformElementWise(d, m, (d1, d2) => d1 % d2);
    }

    public static Matrix operator +(double d, Matrix m)
    {
        return PerformElementWise(d, m, (d1, d2) => d1 + d2);
    }

    public static Matrix operator -(double d, Matrix m)
    {
        return PerformElementWise(d, m, (d1, d2) => d1 - d2);
    }

    public static Matrix operator ^(double d, Matrix m)
    {
        return PerformElementWise(d, m, Math.Pow);
    }


    public static Matrix operator ==(double d, Matrix m)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return PerformElementWise(d, m, (d1, d2) => (d1 == d2) ? 1 : 0);
    }

    public static Matrix operator !=(double d, Matrix m)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return PerformElementWise(d, m, (d1, d2) => (d1 == d2) ? 0 : 1);
    }




    public static Matrix operator *(Matrix m, double d)
    {
        return PerformElementWise(m, d, (d1, d2) => d1 * d2);
    }

    public static Matrix operator /(Matrix m, double d)
    {
        return PerformElementWise(m, d, (d1, d2) => d1 / d2);
    }

    public static Matrix operator %(Matrix m, double d)
    {
        return PerformElementWise(m, d, (d1, d2) => d1 % d2);
    }

    public static Matrix operator +(Matrix m, double d)
    {
        return PerformElementWise(m, d, (d1, d2) => d1 + d2);
    }

    public static Matrix operator -(Matrix m, double d)
    {
        return PerformElementWise(m, d, (d1, d2) => d1 - d2);
    }

    public static Matrix operator ^(Matrix m, double d)
    {
        return PerformElementWise(m, d, Math.Pow);
    }


    public static Matrix operator ==(Matrix m, double d)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return PerformElementWise(m, d, (d1, d2) => (d1 == d2) ? 1 : 0);
    }

    public static Matrix operator !=(Matrix m, double d)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return PerformElementWise(m, d, (d1, d2) => (d1 == d2) ? 0 : 1);
    }
}