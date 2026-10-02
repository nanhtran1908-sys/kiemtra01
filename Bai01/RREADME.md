Câu 1: Sự khác nhau giữa Value Types (Kiểu giá trị) và Reference Types(Kiểu tham chiếu) trong C# về cơ chế lưu trữ vùng nhớ (Stack vs Heap) là
1. Value Types (Kiểu giá trị)
Biến lưu trực tiếp giá trị của dữ liệu.
Khi gán biến này cho biến khác, giá trị được sao chép sang biến mới.
Các kiểu phổ biến: int, double, bool, char, struct, enum.
Với biến cục bộ, giá trị thường được lưu trên Stack.
Hai biến chứa dữ liệu độc lập, thay đổi biến này không làm thay đổi biến còn lại.
2. Reference Types(Kiểu tham chiếu)
Biến không lưu trực tiếp đối tượng, mà lưu địa chỉ tham chiếu đến đối tượng.
Khi gán biến này cho biến khác, tham chiếu được sao chép, nên hai biến có thể cùng trỏ đến một đối tượng.
Các kiểu phổ biến: class, object, string, array, delegate.
Đối tượng được tạo bằng new thường được cấp phát trên Heap; biến tham chiếu có thể nằm trên Stack hoặc là một phần của đối tượng trên Heap tùy ngữ cảnh.
Nếu thay đổi dữ liệu của đối tượng thông qua một biến, biến tham chiếu khác cùng trỏ đến đối tượng đó cũng nhìn thấy thay đổi.
3. Sự khác nhau
Value Types:
Lưu trực tiếp giá trị.
Khi gán sẽ sao chép giá trị.
Hai biến độc lập.
Ví dụ: int, bool, struct, enum.
-> sao chép giá trị
Reference Types:
Lưu tham chiếu đến đối tượng.
Khi gán sẽ sao chép tham chiếu.
Hai biến có thể cùng trỏ đến một đối tượng.
Ví dụ: class, object, array, delegate.
-> sao chép tham chiếu
   
Câu 2: Init-only Properties (init) trong C# 9/10

init là thuộc tính cho phép gán giá trị khi khởi tạo đối tượng. Sau khi đối tượng được tạo, giá trị của thuộc tính không thể thay đổi bằng cách gán lại.
Ví dụ:
class Student
{
    public string Name { get; init; }
}
Student student = new Student
{
    Name = "Ngoc Anh"
};
student.Name = "Nam"; //loi

Trong khi đó, thuộc tính sử dụng set có thể thay đổi giá trị sau khi đối tượng đã được khởi tạo.
class Student
{
    public string Name { get; set; }
}
Student student = new Student
{
    Name = "Ngoc Anh"
};
student.Name = "Nam"; //hop le
Trường hợp sử dụng thực tế:
init phù hợp với những thuộc tính cần xác định ngay khi tạo đối tượng và không muốn thay đổi sau đó, ví dụ như mã sinh viên hoặc mã sự kiện.

Cau 3: Phân biệt virtual và override trong tính Đa hình (Polymorphism)

virtual là từ khóa được sử dụng trong lớp cha để khai báo một phương thức có thể được lớp con thay đổi cách thực hiện.
-> virtual: dùng ở lớp cha, cho phép lớp con ghi đè phương thức.
override là từ khóa được sử dụng trong lớp con để ghi đè và cung cấp cách thực hiện mới cho phương thức virtual của lớp cha.
-> override: dùng ở lớp con, ghi đè phương thức virtual của lớp cha.
Ví dụ:
class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Dong vat phat ra am thanh");
    }
}
class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Cho keu gau gau");
    }
}
Khi gọi:
Animal animal = new Dog();
animal.Sound();
Kết quả:
Cho keu gau gau
Điều này thể hiện tính Đa hình, vì cùng một phương thức Sound() nhưng đối tượng Dog có cách thực hiện riêng.

Câu 4: 

static là thành phần thuộc về lớp (Class) chứ không thuộc về từng đối tượng (Object Instance).
Khi khai báo một thành phần là static, thành phần đó được tạo ra một lần duy nhất và được dùng chung cho tất cả các đối tượng của lớp. Vì vậy, không cần tạo đối tượng bằng new để truy xuất thành phần static.
Ví dụ:
class Student
{
    public static string School = "EPU";
}
Có thể truy xuất trực tiếp thông qua tên lớp:
Console.WriteLine(Student.School);
Không thể truy xuất thông qua đối tượng:
Student student = new Student();
Console.WriteLine(student.School); // Lỗi
-> Thành phần static thuộc về Class, không thuộc về một Object Instance, nên phải truy xuất thông qua tên lớp thay vì thông qua đối tượng được tạo bằng new.
