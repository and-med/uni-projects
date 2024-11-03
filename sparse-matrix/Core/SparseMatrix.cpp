#include "SparseMatrix.h"
//Конструювання матриці
SparseMatrix::SparseMatrix(unsigned t_m, unsigned t_n) : rows(new Element*[t_m]), columns(new Element*[t_n]), m(t_m), n(t_n)
{
    //Ініціалізуємо масив рядків і колонок, і зразу ж закільцовуємо матрицю
    for (unsigned i = 0; i < m; ++i)
    {
        rows[i] = new Element();
        rows[i]->right = rows[i];
    }
    for (unsigned i = 0; i < n; ++i)
    {
        columns[i] = new Element();
        columns[i]->down = columns[i];
    }
}
//Конструктор копіювання
SparseMatrix::SparseMatrix(const SparseMatrix& sp) : rows(new Element*[sp.m]), columns(new Element*[sp.n]), m(sp.m), n(sp.n)
{
    //Робимо дії аналогічно конструюванні матриці
    for (unsigned i = 0; i < m; ++i)
    {
        rows[i] = new Element();
        rows[i]->right = rows[i];
    }
    for (unsigned i = 0; i < n; ++i)
    {
        columns[i] = new Element();
        columns[i]->down = columns[i];
    }
    Element* sliderSp = sp.rows[0]->right;
    Element* slider = rows[0];
    for (unsigned i = 0; i < m; ++i)
    {
        //Налаштовуємо повзунки на початок і-тих рядків
        //матриці з якої копіюється, і матриці в яку копіюватимесь.
        //Причому повзунок матрицї з якої копіюється на одиницю дальше
        sliderSp = sp.rows[i]->right;
        slider = rows[i];
        while (sliderSp != sp.rows[i])
        {
            //тепер зразуж після повзунка вставляємо елемент
            //на який вказує повзунок матриці з якої копіюється
            slider->right = new Element(*sliderSp);
            slider = slider->right;
            sliderSp = sliderSp->right;
            //тепер починаючи з j-го рядка шукаємо де потрібно поміняти значення вказівників
            //щоби матриця залишалась правильної і для стовпчиків
            Element* columnsSliderCurr = columns[slider->j]->down;
            Element* columnsSliderPrev = columns[slider->j];
            while (columnsSliderCurr->i < slider->i && columnsSliderCurr != columns[slider->j])
            {
                columnsSliderPrev = columnsSliderCurr;
                columnsSliderCurr = columnsSliderPrev->down;
            }
            columnsSliderPrev->down = slider;
            slider->down = columnsSliderCurr;
        }
        //Кінець рядка вказуємо на початок рядка
        slider->right = rows[i];
    }
}
//Деструктор
SparseMatrix::~SparseMatrix()
{
    //Видаляємо по черзі в кожному рядку елементи один за одним
    for (unsigned i = 0; i < m; ++i)
    {
        Element* current = rows[i]->right;
        while (current != rows[i])
        {
            Element* oldCurrent = current;
            current = current->right;
            delete oldCurrent;
        }
    }
    //Видаляємо елементи, які знаходяться в масиві стовпців і рядків
    for (unsigned i = 0; i < m; ++i)
    {
        delete rows[i];
    }
    for (unsigned i = 0; i < n; ++i)
    {
        delete columns[i];
    }
    //Видаляємо масив стовпців і рядків
    delete[] rows;
    delete[] columns;
}
//Перевірка на рівність (необхідно для тестування)
bool SparseMatrix::operator==(const SparseMatrix& sp)const
{
    //якщо не рівна кількість рядків або стовпчиків, то матриці не рівні
    if (m != sp.m || n != sp.n) return false;
    for (unsigned i = 0; i < sp.m; ++i)
    {
        Element* currentLeft = rows[i]->right;
        Element* currentRight = sp.rows[i]->right;
        while (currentLeft != rows[i] || currentRight != sp.rows[i])
        {
            //якщо індекси, або значення відповідних елементів не рівні,
            //то матриці теж не рівні
            if (currentLeft->j != currentRight->j || currentLeft->value != currentRight->value) return false;
            currentLeft = currentLeft->right;
            currentRight = currentRight->right;
        }
    }
    //якщо всі відповідні елементи рівні, то матриці рівні
    return true;
}
//Перевірка на нерівність
bool SparseMatrix::operator!=(const SparseMatrix& sp)const
{
    //якщо не рівна кількість рядків або стовпчиків, то матриці не рівні
    if (m != sp.m || n != sp.n) return true;
    for (unsigned i = 0; i < sp.m; ++i)
    {
        Element* currentLeft = rows[i]->right;
        Element* currentRight = sp.rows[i]->right;
        while (currentLeft != rows[i] || currentRight != sp.rows[i])
        {
            //якщо індекси, або значення відповідних елементів не рівні,
            //то матриці теж не рівні
            if (currentLeft->j != currentRight->j || currentLeft->value != currentRight->value) return true;
            currentLeft = currentLeft->right;
            currentRight = currentRight->right;
        }
    }
    //якщо всі відповідні елементи рівні, то матриці рівні
    return false;
}
//оператор для отримання i-того, j-того елемента
int SparseMatrix::operator() (unsigned i, unsigned j)
{
    if (i >= m || j >= n) return 0;
    Element* slider = rows[i]->right;
    while (slider->j < j && slider != rows[i])
    {
        slider = slider->right;
    }
    if (slider->j == j && slider != rows[i])
    {
        return slider->value;
    }
    else return 0;
}
//траспонування матриці
SparseMatrix& SparseMatrix::transpose()
{
    //Конструюємо матрицю nxm
    SparseMatrix newThis(n, m);
    //Діємо майже аналогічно конструктору копіюванню
    //тільки повзунок матриці з якої копіюємо налаштовуємо на рядок
    //щоби копіювати з першого рядка в перший стовпчик
    Element* sliderSp = columns[0]->down;
    Element* slider = newThis.rows[0];
    for (unsigned i = 0; i < n; ++i)
    {
        sliderSp = columns[i]->down;
        slider = newThis.rows[i];
        while (sliderSp != columns[i])
        {
            //Повзунок вказує на аналогічний елемент в відповідному стовпчику
            //матриці з якої копіюємо тільки з поміняними значеннями i i j
            slider->right = new Element(*sliderSp);
            std::swap(slider->right->i, slider->right->j);
            slider = slider->right;
            sliderSp = sliderSp->down;
            //тепер починаючи з j-го рядка шукаємо де потрібно поміняти значення вказівників
            //щоби матриця залишалась правильної і для стовпчиків
            Element* columnsSliderCurr = newThis.columns[slider->j]->down;
            Element* columnsSliderPrev = newThis.columns[slider->j];
            while (columnsSliderCurr->i < slider->i && columnsSliderCurr != newThis.columns[slider->j])
            {
                columnsSliderPrev = columnsSliderCurr;
                columnsSliderCurr = columnsSliderPrev->down;
            }
            columnsSliderPrev->down = slider;
            slider->down = columnsSliderCurr;
        }
        //Кінець рядка вказуємо на початок рядка
        slider->right = newThis.rows[i];
    }
    *this = newThis;
    return *this;
}
//оператор присвоєння
SparseMatrix& SparseMatrix::operator=(const SparseMatrix& sp)
{
    //Спочатку потрібно почистити матрицю, в яку ми копіюємо
    for (unsigned i = 0; i < m; ++i)
    {
        Element* current = rows[i]->right;
        while (current != rows[i])
        {
            Element* oldCurrent = current;
            current = current->right;
            delete oldCurrent;
        }
    }
    for (unsigned i = 0; i < m; ++i)
    {
        delete rows[i];
    }
    for (unsigned i = 0; i < n; ++i)
    {
        delete columns[i];
    }
    delete[] rows;
    delete[] columns;
    //тепер конструюємо нову матрицю потрібного розміру
    rows = new Element*[sp.m];
    columns = new Element*[sp.n];
    m = sp.m;
    n = sp.n;
    for (unsigned i = 0; i < m; ++i)
    {
        rows[i] = new Element();
        rows[i]->right = rows[i];
    }
    for (unsigned i = 0; i < n; ++i)
    {
        columns[i] = new Element();
        columns[i]->down = columns[i];
    }
    //Виконуємо дії аналогічні конструктору копіювання
    Element* sliderSp = sp.rows[0]->right;
    Element* slider = rows[0];
    for (unsigned i = 0; i < m; ++i)
    {
        sliderSp = sp.rows[i]->right;
        slider = rows[i];
        while (sliderSp != sp.rows[i])
        {
            slider->right = new Element(*sliderSp);
            slider = slider->right;
            sliderSp = sliderSp->right;
            Element* columnsSliderCurr = columns[slider->j]->down;
            Element* columnsSliderPrev = columns[slider->j];
            while (columnsSliderCurr->i < slider->i && columnsSliderCurr != columns[slider->j])
            {
                columnsSliderPrev = columnsSliderCurr;
                columnsSliderCurr = columnsSliderPrev->down;
            }
            columnsSliderPrev->down = slider;
            slider->down = columnsSliderCurr;
        }
        slider->right = rows[i];
    }
    return *this;
}
//Присвоєння з додаванням
SparseMatrix& SparseMatrix::operator+= (const SparseMatrix& sp)
{
    //якщо розміри матриць не рівні, то матрицю не змінюємо
    if (m != sp.m || n != sp.n) return *this;
    for (unsigned i = 0; i < m; ++i)
    {
        //Налаштовуємо повзунки правої і лівої матриць на початок рядків
        Element* sliderLeft = rows[i]->right;
        Element* sliderRight = sp.rows[i]->right;
        Element* leftPrevious = rows[i];
        //Поки два повзунки не вказують на початок рядків
        //виконуємо доавання рядків
        while ((sliderLeft != rows[i] || sliderRight != sp.rows[i]))
        {
            //Поки в правій матриці йдуть нулі на відповідних місцях лівої матриці,
            //тобто повзунок в правій матриці відносно дальше ніж в лівій,
            //то просто просуваємо повзунок лівої матриці, нічого не змінюючи
            while ((sliderLeft->j < sliderRight->j || sliderRight == sp.rows[i]) && sliderLeft != rows[i])
            {
                sliderLeft = sliderLeft->right;
                leftPrevious = leftPrevious->right;
            }
            //Поки в лівій матриці йдуть нулі на відповідних місцях правої матриці,
            //тобто повзунок в лівій матриці відносно дальше ніж в правій,
            //то вставляємо елементи на які вказує повзунокв правій матриці в ліву матрицю, і просуваємо повзунок
            while ((sliderRight->j < sliderLeft->j || sliderLeft == rows[i]) && sliderRight != sp.rows[i])
            {
                leftPrevious->right = new Element(*sliderRight);
                leftPrevious = leftPrevious->right;
                leftPrevious->right = sliderLeft;
                Element* columnsSliderCurr = columns[leftPrevious->j]->down;
                Element* columnsSliderPrev = columns[leftPrevious->j];
                while (columnsSliderCurr->i < leftPrevious->i && columnsSliderCurr != columns[leftPrevious->j])
                {
                    columnsSliderPrev = columnsSliderCurr;
                    columnsSliderCurr = columnsSliderPrev->down;
                }
                columnsSliderPrev->down = leftPrevious;
                leftPrevious->down = columnsSliderCurr;
                sliderRight = sliderRight->right;
            }
            //якщо повзунки на однакових відповідних позиціях, то просто
            //збільшуємо значення елемента на який вказує повзунок в лівій матриці,
            //або видаляємо елемент якщо сума дає 0,
            //і просуваємо два повзунки
            if (sliderLeft->j == sliderRight->j && sliderLeft != rows[i] && sliderRight != sp.rows[i])
            {
                if (sliderRight->value + sliderLeft->value == 0)
                {
                    Element* NewSlider = sliderLeft->right;
                    deleteElem(sliderLeft->i, sliderLeft->j);
                    sliderLeft = NewSlider;
                    sliderRight = sliderRight->right;
                }
                else
                {
                    sliderLeft->value = sliderLeft->value + sliderRight->value;
                    sliderLeft = sliderLeft->right;
                    leftPrevious = leftPrevious->right;
                    sliderRight = sliderRight->right;
                }
            }
        }
    }
    return *this;
}
//Присвоєння з відніманням
SparseMatrix& SparseMatrix::operator-= (const SparseMatrix& sp)
{
    //якщо розміри матриць не рівні, то матрицю не змінюємо
    if (m != sp.m || n != sp.n) return *this;
    for (unsigned i = 0; i < m; ++i)
    {
        Element* sliderLeft = rows[i]->right;
        Element* sliderRight = sp.rows[i]->right;
        Element* leftPrevious = rows[i];
        //Поки два повзунки не вказують на початок рядків
        //виконуємо віднімання рядків
        while ((sliderLeft != rows[i] || sliderRight != sp.rows[i]))
        {
            //Поки в правій матриці йдуть нулі на відповідних місцях лівої матриці,
            //тобто повзунок в правій матриці відносно дальше ніж в лівій,
            //то просто просуваємо повзунок лівої матриці, нічого не змінюючи
            while ((sliderLeft->j < sliderRight->j || sliderRight == sp.rows[i]) && sliderLeft != rows[i])
            {
                sliderLeft = sliderLeft->right;
                leftPrevious = leftPrevious->right;
            }
            //Поки в лівій матриці йдуть нулі на відповідних місцях правої матриці,
            //тобто повзунок в лівій матриці відносно дальше ніж в правій,
            //то вставляємо елементи на які вказує повзунокв правій матриці в ліву матрицю
            //тільки з відємним значенням, і просуваємо повзунок
            while ((sliderRight->j < sliderLeft->j || sliderLeft == rows[i]) && sliderRight != sp.rows[i])
            {
                leftPrevious->right = new Element(*sliderRight);
                leftPrevious->right->value = -leftPrevious->right->value;
                leftPrevious = leftPrevious->right;
                leftPrevious->right = sliderLeft;
                Element* columnsSliderCurr = columns[leftPrevious->j]->down;
                Element* columnsSliderPrev = columns[leftPrevious->j];
                while (columnsSliderCurr->i < leftPrevious->i && columnsSliderCurr != columns[leftPrevious->j])
                {
                    columnsSliderPrev = columnsSliderCurr;
                    columnsSliderCurr = columnsSliderPrev->down;
                }
                columnsSliderPrev->down = leftPrevious;
                leftPrevious->down = columnsSliderCurr;
                sliderRight = sliderRight->right;
            }
            //якщо повзунки на однакових відповідних позиціях, то просто
            //збільшуємо значення елемента на який вказує повзунок в лівій матриці,
            //або видаляємо елемент якщо сума дає 0,
            //і просуваємо два повзунки
            if (sliderLeft->j == sliderRight->j && sliderLeft != rows[i] && sliderRight != sp.rows[i])
            {
                if (sliderRight->value - sliderLeft->value == 0)
                {
                    Element* NewSlider = sliderLeft->right;
                    deleteElem(sliderLeft->i, sliderLeft->j);
                    sliderLeft = NewSlider;
                    sliderRight = sliderRight->right;
                }
                else
                {
                    sliderLeft->value = sliderLeft->value - sliderRight->value;
                    sliderLeft = sliderLeft->right;
                    leftPrevious = leftPrevious->right;
                    sliderRight = sliderRight->right;
                }
            }
        }
    }
    return *this;
}
//Присвоєння з домноженням
SparseMatrix& SparseMatrix::operator*= (const SparseMatrix& sp)
{
    if (n != sp.m) return *this;
    SparseMatrix resultMatrix(m, sp.n);
    for (unsigned i = 0; i < m; ++i)
    {
        int result = 0;
        Element* sliderLeft = rows[i]->right;
        for (unsigned j = 0; j < sp.n; j++)
        {
            result = 0;
            sliderLeft = rows[i]->right;
            Element* sliderRight = sp.columns[j]->down;
            //Поки два повзунки не вказують на початок рядків
            //виконуємо множення рядків
            while ((sliderLeft != rows[i] || sliderRight != sp.columns[j]))
            {
                //Поки в правій матриці йдуть нулі на відповідних місцях лівої матриці,
                //тобто повзунок в правій матриці відносно дальше ніж в лівій,
                //то просуваємо повзунок лівої матриці, нічого не змінюючи
                while ((sliderLeft->j < sliderRight->i || sliderRight == sp.columns[j]) && sliderLeft != rows[i])
                {
                    sliderLeft = sliderLeft->right;
                }
                //Поки в лівій матриці йдуть нулі на відповідних місцях правої матриці,
                //тобто повзунок в лівій матриці відносно дальше ніж в правій,
                //то просуваємо повзунок правої матриці, нічого не змінюючи
                while ((sliderRight->i < sliderLeft->j || sliderLeft == rows[i]) && sliderRight != sp.columns[j])
                {
                    sliderRight = sliderRight->down;
                }
                //якщо повзунки на однакових відповідних позиціях, то
                //додаємо добуток відповідних значень до результату множення двох рядків,
                //і просуваємо два повзунки
                if (sliderLeft->j == sliderRight->i && sliderLeft != rows[i] && sliderRight != sp.columns[j])
                {
                    result += sliderLeft->value*sliderRight->value;
                    sliderLeft = sliderLeft->right;
                    sliderRight = sliderRight->down;
                }
            }
            //Результат множення i-того рядка на j-стопчик додаємо до результуючої матриці
            resultMatrix.add(result, i, j);
        }
    }
    *this = resultMatrix;
    return *this;
}
//Домноження матриці на число
SparseMatrix& SparseMatrix::operator*= (const int& rv)
{
    //Проходимось по кожному елементу і його значення домножуємо на задане число
    for (unsigned i = 0; i < m; ++i)
    {
        Element* current = rows[i]->right;
        while (current != rows[i])
        {
            if (rv == 0)
            {
                Element* oldCurrent = current;
                current = current->right;
                deleteElem(oldCurrent->i, oldCurrent->j);
            }
            else
            {
                current->value *= rv;
                current = current->right;
            }
        }
    }
    return *this;
}
//Додавання матриць
SparseMatrix operator+ (SparseMatrix lv, const SparseMatrix& rv)
{
    return lv += rv;
}
//Віднімання матриць
SparseMatrix operator- (SparseMatrix lv, const SparseMatrix& rv)
{
    return lv -= rv;
}
//Множення матриць
SparseMatrix operator* (SparseMatrix lv, const SparseMatrix& rv)
{
    return lv *= rv;
}
//Множення матриці на число
SparseMatrix operator* (SparseMatrix lv, const int& rv)
{
    //Проходимось по кожному елементу і його значення домножуємо на задане число
    for (unsigned i = 0; i < lv.m; ++i)
    {
        Element* current = lv.rows[i]->right;
        while (current != lv.rows[i])
        {
            if (rv == 0)
            {
                Element* oldCurrent = current;
                current = current->right;
                lv.deleteElem(oldCurrent->i, oldCurrent->j);
            }
            else
            {
                current->value *= rv;
                current = current->right;
            }
        }
    }
    return lv;
}
//Множення числа наматрицю
SparseMatrix operator* (const int& lv, SparseMatrix rv){
    //Проходимось по кожному елементу і його значення домножуємо на задане число
    for (unsigned i = 0; i < rv.m; ++i)
    {
        Element* current = rv.rows[i]->right;
        while (current != rv.rows[i])
        {
            if (lv == 0)
            {
                Element* oldCurrent = current;
                current = current->right;
                rv.deleteElem(oldCurrent->i, oldCurrent->j);
            }
            else
            {
                current->value *= lv;
                current = current->right;
            }
        }
    }
    return rv;
}
//Видалення елемента за його індексами
void SparseMatrix::deleteElem(unsigned i, unsigned j)
{
    //Спочатку знаходимо елемент який потрібно видалити по рядку
    //і змінюємо значення вказівника направо попереднього елемента,
    //причому перевіряємо чи взагалі такий елемент існує
    Element* current = rows[i]->right;
    Element* previous = rows[i];
    while (current->j < j && current != rows[i])
    {
        previous = current;
        current = current->right;
    }
    if (current->j == j && current != rows[i])
    {
        previous->right = current->right;
    }
    else
    {
        return;
    }
    //тепер знаходимо цей же елемент по стовпчику
    //і відповідно змінюємо вказівник попереднього елементу
    current = columns[j]->down;
    previous = columns[j];
    while (current->i < i && current != columns[i])
    {
        previous = current;
        current = current->down;
    }
    previous->down = current->down;
    //тепер видаляємо елемент
    delete current;
}
//Додавання елемента за його індексами
void SparseMatrix::add(int value, unsigned i, unsigned j)
{
    if (value != 0)
    {
        //якщо елемент не нульовий, то вставляємо його на відповідне місце по рядку
        Element* newElem = new Element(i, j, value);
        Element* current = rows[i]->right;
        Element* previous = rows[i];
        while (current->j < newElem->j && current != rows[i])
        {
            previous = current;
            current = current->right;
        }
        if (current->j == newElem->j && current != rows[i])
        {
            current->value = newElem->value;
            return;
        }
        previous->right = newElem;
        newElem->right = current;
        //І змінюємо вказівники відповідних елементів по рядку
        //для збереження структури матриці
        current = columns[j]->down;
        previous = columns[j];
        while (current->i < newElem->i && current != columns[j])
        {
            previous = current;
            current = current->down;
        }
        previous->down = newElem;
        newElem->down = current;
    }
}
//оператор виведення матриці
std::ostream& operator << (std::ostream& out, const SparseMatrix& sp)
{
    //Робимо цикл так якби матриця була повна
    //якщо елемента на відповідному кроці не існує виводимо нуль
    //Інакше виводимо значення цього елемента
    for (unsigned i = 0; i < sp.m; ++i)
    {
        Element* current = sp.rows[i]->right;
        for (unsigned j = 0; j < sp.n; ++j)
        {
            if (current->j > j || current == (sp.rows[i]))
            {
                out << "0 ";
            }
            else
            {
                out << current->value << " ";
                current = current->right;
            }
        }
        out << std::endl;
    }
    return out;
}
//оператор зчитування матриці
std::istream& operator >> (std::istream& in, SparseMatrix& sp)
{
    int value;
    for (unsigned i = 0; i < sp.m; ++i)
    {
        for (unsigned j = 0; j < sp.n; ++j)
        {
            in >> value;
            sp.add(value, i, j);
        }
    }
    return in;
}