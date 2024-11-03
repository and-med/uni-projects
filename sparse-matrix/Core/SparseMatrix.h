#ifndef CORE_LIBRARY_H
#define CORE_LIBRARY_H

#include "Element.h"
#include <iostream>

class SparseMatrix
{
    Element** rows;
    Element** columns;
    unsigned m;
    unsigned n;
    void deleteElem(unsigned, unsigned);
public:
    SparseMatrix() : rows(nullptr), columns(nullptr), m(0), n(0) {}
    SparseMatrix(unsigned, unsigned);
    SparseMatrix(const SparseMatrix&);
    ~SparseMatrix();
    void add(int, unsigned, unsigned);
    SparseMatrix& transpose();
    bool operator== (const SparseMatrix&)const;
    bool operator!= (const SparseMatrix&)const;
    int operator() (unsigned, unsigned);
    SparseMatrix& operator= (const SparseMatrix&);
    SparseMatrix& operator+= (const SparseMatrix&);
    SparseMatrix& operator-= (const SparseMatrix&);
    SparseMatrix& operator*= (const SparseMatrix&);
    SparseMatrix& operator*= (const int&);
    friend SparseMatrix operator+ (SparseMatrix, const SparseMatrix&);
    friend SparseMatrix operator- (SparseMatrix, const SparseMatrix&);
    friend SparseMatrix operator* (SparseMatrix, const SparseMatrix&);
    friend SparseMatrix operator* (SparseMatrix, const int&);
    friend SparseMatrix operator* (const int&, SparseMatrix);
    friend std::ostream& operator << (std::ostream& out, const SparseMatrix&);
    friend std::istream& operator >> (std::istream& in, SparseMatrix&);
};

#endif //CORE_LIBRARY_H
