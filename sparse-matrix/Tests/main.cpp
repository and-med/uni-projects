#include "stdafx.h"
#include "CppUnitTest.h"
#include "SparseMatrix.h"
#include "D:\studio_projects\1.ALGORITHM\SparseMatrices\SparseMatrices\SparseMatrix.cpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace SparseMatrixTest
{
    TEST_CLASS(UnitTest1)
            {
                public:
                void FillBigMarix(SparseMatrix& actualMatrix1, SparseMatrix& actualMatrix2)
                {
                    actualMatrix1.add(1, 465, 51);
                    actualMatrix1.add(-2, 322, 121);
                    actualMatrix1.add(4, 501, 505);
                    actualMatrix1.add(3, 81, 777);
                    actualMatrix1.add(-5, 888, 138);
                    actualMatrix1.add(6, 329, 0);
                    actualMatrix1.add(23, 171, 229);
                    actualMatrix1.add(18, 322, 665);

                    actualMatrix2.add(14, 665, 113);
                    actualMatrix2.add(8, 121, 335);
                    actualMatrix2.add(6, 81, 777);
                    actualMatrix2.add(5, 0, 0);
                    actualMatrix2.add(-8, 229, 14);
                    actualMatrix2.add(2, 501, 505);
                    actualMatrix2.add(1, 51, 325);
                    actualMatrix2.add(4, 322, 121);
                }
                void fillMatrix(SparseMatrix& actualMatrix1, SparseMatrix& actualMatrix2)
                {
                    actualMatrix1.add(1, 0, 0);
                    actualMatrix1.add(2, 0, 1);
                    actualMatrix1.add(4, 1, 0);
                    actualMatrix1.add(3, 2, 2);

                    actualMatrix2.add(2, 1, 2);
                    actualMatrix2.add(1, 0, 0);
                    actualMatrix2.add(4, 2, 2);
                }

                TEST_METHOD(add)
                {
                    SparseMatrix actualMatrix1(3, 3);
                    SparseMatrix actualMatrix2(3, 3);
                    SparseMatrix expectedMat(3, 3);

                    fillMatrix(actualMatrix1, actualMatrix2);

                    expectedMat.add(2, 0, 0);
                    expectedMat.add(2, 0, 1);
                    expectedMat.add(4, 1, 0);
                    expectedMat.add(2, 1, 2);
                    expectedMat.add(7, 2, 2);

                    bool t = (actualMatrix1 + actualMatrix2) == expectedMat;

                    Assert::AreEqual(t, true);

                }

                TEST_METHOD(substraction)
                {
                    SparseMatrix actualMatrix1(3, 3);
                    SparseMatrix actualMatrix2(3, 3);
                    SparseMatrix expectedMat(3, 3);
                    fillMatrix(actualMatrix1, actualMatrix2);
                    expectedMat.add(0, 0, 0);
                    expectedMat.add(2, 0, 1);
                    expectedMat.add(4, 1, 0);
                    expectedMat.add(-2, 1, 2);
                    expectedMat.add(-1, 2, 2);
                    bool t = (actualMatrix1 - actualMatrix2) == expectedMat;
                    Assert::AreEqual(t, true);
                }

                TEST_METHOD(multiplication)
                {
                    SparseMatrix actualMatrix1(3, 3);
                    SparseMatrix actualMatrix2(3, 3);
                    SparseMatrix expectedMat(3, 3);
                    fillMatrix(actualMatrix1, actualMatrix2);
                    expectedMat.add(1, 0, 0);
                    expectedMat.add(4, 0, 2);
                    expectedMat.add(4, 1, 0);
                    expectedMat.add(12, 2, 2);
                    bool t = (actualMatrix1 * actualMatrix2) == expectedMat;
                    Assert::AreEqual(t, true);
                }

                TEST_METHOD(transposition)
                {
                    SparseMatrix actualMatrix1(3, 3);
                    SparseMatrix actualMatrix2(3, 3);
                    SparseMatrix expectedMat(3, 3);
                    fillMatrix(actualMatrix1, actualMatrix2);
                    expectedMat.add(1, 0, 0);
                    expectedMat.add(2, 1, 0);
                    expectedMat.add(4, 0, 1);
                    expectedMat.add(3, 2, 2);
                    actualMatrix1.transpose();
                    bool t = actualMatrix1 == expectedMat;
                    Assert::AreEqual(t, true);
                }

                TEST_METHOD(getElement)
                {
                    SparseMatrix actualMatrix1(3, 3);
                    SparseMatrix actualMatrix2(3, 3);
                    fillMatrix(actualMatrix1, actualMatrix2);
                    Assert::AreEqual(actualMatrix1(1, 1), actualMatrix2(1, 1));
                }

                TEST_METHOD(multiplication2)
                {
                    const int multiplier = 4;
                    SparseMatrix actualMatrix1(3, 3);
                    SparseMatrix expectedMat(3, 3);
                    actualMatrix1.add(1, 0, 0);
                    actualMatrix1.add(2, 0, 1);
                    actualMatrix1.add(4, 1, 0);
                    actualMatrix1.add(3, 2, 2);
                    expectedMat.add(4, 0, 0);
                    expectedMat.add(8, 0, 1);
                    expectedMat.add(16, 1, 0);
                    expectedMat.add(12, 2, 2);
                    actualMatrix1 *= multiplier;
                    bool t = expectedMat == actualMatrix1;
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixAdd)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix actualMatrix2(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    FillBigMarix(actualMatrix1, actualMatrix2);
                    expectedMat.add(1, 465, 51);
                    expectedMat.add(2, 322, 121);
                    expectedMat.add(6, 501, 505);
                    expectedMat.add(9, 81, 777);
                    expectedMat.add(-5, 888, 138);
                    expectedMat.add(6, 329, 0);
                    expectedMat.add(23, 171, 229);
                    expectedMat.add(18, 322, 665);
                    expectedMat.add(14, 665, 113);
                    expectedMat.add(8, 121, 335);
                    expectedMat.add(5, 0, 0);
                    expectedMat.add(-8, 229, 14);
                    expectedMat.add(1, 51, 325);
                    bool t = ((actualMatrix1 + actualMatrix2) == expectedMat);
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixSubstraction)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix actualMatrix2(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    FillBigMarix(actualMatrix1, actualMatrix2);
                    expectedMat.add(1, 465, 51);
                    expectedMat.add(-6, 322, 121);
                    expectedMat.add(2, 501, 505);
                    expectedMat.add(-3, 81, 777);
                    expectedMat.add(-5, 888, 138);
                    expectedMat.add(6, 329, 0);
                    expectedMat.add(23, 171, 229);
                    expectedMat.add(18, 322, 665);
                    expectedMat.add(-14, 665, 113);
                    expectedMat.add(-8, 121, 335);
                    expectedMat.add(-5, 0, 0);
                    expectedMat.add(8, 229, 14);
                    expectedMat.add(-1, 51, 325);
                    bool t = ((actualMatrix1 - actualMatrix2) == expectedMat);
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixMultiplication)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix actualMatrix2(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    FillBigMarix(actualMatrix1, actualMatrix2);
                    expectedMat.add(1, 465, 325);
                    expectedMat.add(-16, 322, 335);
                    expectedMat.add(30, 329, 0);
                    expectedMat.add(-184, 171, 14);
                    expectedMat.add(252, 322, 113);
                    bool t = ((actualMatrix1 * actualMatrix2) == expectedMat);
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixMultiplication2)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    actualMatrix1.add(1, 465, 51);
                    actualMatrix1.add(-2, 322, 121);
                    actualMatrix1.add(4, 501, 505);
                    actualMatrix1.add(3, 81, 777);
                    actualMatrix1.add(-5, 888, 138);
                    actualMatrix1.add(6, 329, 0);
                    actualMatrix1.add(23, 171, 229);
                    actualMatrix1.add(18, 322, 665);

                    expectedMat.add(8, 465, 51);
                    expectedMat.add(-16, 322, 121);
                    expectedMat.add(32, 501, 505);
                    expectedMat.add(24, 81, 777);
                    expectedMat.add(-40, 888, 138);
                    expectedMat.add(48, 329, 0);
                    expectedMat.add(184, 171, 229);
                    expectedMat.add(144, 322, 665);
                    actualMatrix1 *= 8;
                    bool t = (actualMatrix1 == expectedMat);
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixZeroMultiplication2)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    actualMatrix1.add(1, 465, 51);
                    actualMatrix1.add(-2, 322, 121);
                    actualMatrix1.add(4, 501, 505);
                    actualMatrix1.add(3, 81, 777);
                    actualMatrix1.add(-5, 888, 138);
                    actualMatrix1.add(6, 329, 0);
                    actualMatrix1.add(23, 171, 229);
                    actualMatrix1.add(18, 322, 665);

                    actualMatrix1 *= 0;
                    bool t = (actualMatrix1 == expectedMat);
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixZeroMultiplication)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix actualMatrix2(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    actualMatrix1.add(1, 465, 51);
                    actualMatrix1.add(-2, 322, 121);
                    actualMatrix1.add(4, 501, 505);
                    actualMatrix1.add(3, 81, 777);
                    actualMatrix1.add(-5, 888, 138);
                    actualMatrix1.add(6, 329, 0);
                    actualMatrix1.add(23, 171, 229);
                    actualMatrix1.add(18, 322, 665);

                    bool t = ((actualMatrix1*actualMatrix2) == expectedMat);
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixZeroAdd)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix actualMatrix2(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    actualMatrix1.add(1, 465, 51);
                    actualMatrix1.add(-2, 322, 121);
                    actualMatrix1.add(4, 501, 505);
                    actualMatrix1.add(3, 81, 777);
                    actualMatrix1.add(-5, 888, 138);
                    actualMatrix1.add(6, 329, 0);
                    actualMatrix1.add(23, 171, 229);
                    actualMatrix1.add(18, 322, 665);

                    expectedMat = actualMatrix1;
                    bool t = ((actualMatrix1+actualMatrix2) == expectedMat);
                    Assert::AreEqual(t, true);
                }
                TEST_METHOD(BigMatrixZeroSubstraction)
                {
                    SparseMatrix actualMatrix1(1000, 1000);
                    SparseMatrix actualMatrix2(1000, 1000);
                    SparseMatrix expectedMat(1000, 1000);
                    actualMatrix1.add(1, 465, 51);
                    actualMatrix1.add(-2, 322, 121);
                    actualMatrix1.add(4, 501, 505);
                    actualMatrix1.add(3, 81, 777);
                    actualMatrix1.add(-5, 888, 138);
                    actualMatrix1.add(6, 329, 0);
                    actualMatrix1.add(23, 171, 229);
                    actualMatrix1.add(18, 322, 665);

                    expectedMat = actualMatrix1;
                    bool t = ((actualMatrix1 - actualMatrix2) == expectedMat);
                    Assert::AreEqual(t, true);
                }
        };
}