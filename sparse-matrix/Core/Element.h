#ifndef SPARSEMATRIX_ELEMENT_H
#define SPARSEMATRIX_ELEMENT_H

struct Element
{
    unsigned i;
    unsigned j;
    int value;
    Element* right;
    Element* down;

    Element() : i(0), j(0), value(0), right(nullptr), down(nullptr) {}
    Element(unsigned t_i, unsigned t_j, int t_value) : i(t_i), j(t_j), value(t_value), right(nullptr), down(nullptr) {}
    Element(const Element& elem) : i(elem.i), j(elem.j), value(elem.value), right(nullptr), down(nullptr) {}
};

#endif //SPARSEMATRIX_ELEMENT_H
