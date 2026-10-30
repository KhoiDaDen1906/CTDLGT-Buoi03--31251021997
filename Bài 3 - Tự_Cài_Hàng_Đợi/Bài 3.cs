using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
public class CircularQueue<T>
{
    // Các biến để quản lý hàng đợi vòng
    private T [] items;
    private int front;
    private int rear;
    private int count;

    // Các thuộc tính liên kết trực tiếp với dữ liệu nội bộ
    // Số lượng phần tử có trong mảng
    public int Count => count;
    // Dung lượng của mảng hiện tại 
    public int Capacity => items.Length;

    //Hàm khởi tạo mảng 4 ô ban đầu và đặt các chỉ số về 0
    public CircularQueue()
    {
        items = new T[4];
        front = 0;
        rear = 0;
        count = 0;
    }

    // Khởi tạo hàm chức năng Peek().
    public T Peek()
    {
        // Xét trường hợp nếu mảng rỗng thì trả về lỗi.
        if (count == 0)
        {
            throw new InvalidOperationException("Hàng đợi rỗng!");
        }
        // Nếu vượt qua được if thì trả về giá trị ở đầu hàng.
        return items[front];
    }

    // Khởi tạo hàm chức năng Dequeue() - Lấy và Xoá phần tử ở đầu mảng.
    public T Dequeue()
    {
        // Kiểm tra mảng có rỗng hay không trước.
        if (count == 0)
        {
            throw new InvalidOperationException("Hàng đợi rỗng!");
        }
        // Lưu lại giá trị của phần tử ở vị trí đầu hàng vào biến kết quả để in ra.
        T result = items[front];

        // Xoá phần tử vừa lấy ra (gán null nếu là object hoặc string, 0 nếu là số)
        // Mục đích để loại bỏ đi ô vừa xoá phần tử giảm thiểu việc rò rỉ RAM.
        items[front] = default(T);

        // Nhích đầu hàng sang phải 1 ô.
        // Dùng phép chia lấy dư để front quay về vị trí đầu là 0 nếu đi đến cuối hàng.
        front = (front + 1) % items.Length;

        // Cập nhật lại số lượng phần tử.
        count --;

        // Trả về kết quả là giá trị tại phần tử front đầu hàng.
        return result;
    }
    // Hàm tăng dung lượng của hàng đợi lên 2 lần khi hàng đầy.
    private void Resize()
    {
        // Tạo mảng mới to gấp đôi mảng cũ.
        T[] newItems = new T[items.Length * 2];

        // Chuyển toàn bộ dữ liệu từ mảng cũ sang mảng mới theo đúng thứ tự sắp xếp từ vị trí đầu hàng.
        for (int i = 0 ; i < count ; i++)
        {
            // Công thức giúp duyệt các phần tử của mảng cũ bắt đầu từ vị trí front đến hết.
            newItems[i] = items[(front + i) % items.Length];
        }
        
        // Chuyển sang làm việc ở mảng mới to hơn.
        items = newItems;

        // Vì đã chuyển toàn bộ sang mảng mới nên ta đổi chỉ số lại bắt đầu từ 0
        front = 0;
        // Vị trí cho ô trống tiếp theo để đón khách.
        rear = count;
    }
    public void Enqueue(T value)
    {
        // Trước khi thêm vào ta phải xem xem mảng có còn chỗ để thêm vào không.
        // Nếu hết chỗ thì gọi hàm tăng dung lượng.
        if (count == items.Length)
        {
            Resize();
        }
        
        // Đặt phần tử mới vào ô cuối hàng, chỗ của rear.
        items[rear] = value;

        // Dịch chuyển rear lên ô tiếp theo theo vòng tròn bằng cách sử dụng phép chia lấy dư.
        rear = (rear + 1) % items.Length;

        // Tăng biến count để ghi nhận có thêm người vào hàng.
        count ++;
    }
   
}

public class Queue_setting_by_myself
{ 
    private static void Main(string[] args)
    {
       // TESTCASE 1: Hàng rỗng

       System.Console.WriteLine("TESTCASE 1: KIỂM TRA HÀNG RỖNG");
       var q1 = new CircularQueue<string>();
       Console.WriteLine($"Khởi tạo: Count = {q1.Count}, Capacity = {q1.Capacity}");
        // Dùng try-catch để bắt lỗi.
        try
        {
            Console.Write("TEST gọi Dequeue(): ");
            q1.Dequeue();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Bắt thành công lỗi! -> {ex.Message}");
        }

        try
        {
            Console.Write("TEST gọi Peek(): ");
            q1.Peek();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Bắt thành công lỗi! -> {ex.Message}");
        } 

        // TESTCASE 2

        Console.WriteLine("TESTCASE 2: VỪA ĐẦY");
        var q2 = new CircularQueue<string>();
        q2.Enqueue("A");
        q2.Enqueue("B");
        q2.Enqueue("C");
        q2.Enqueue("D");
        Console.WriteLine($"Đã Enqueue A, B, C, D -> Count = {q2.Count}, Capacity = {q2.Capacity}");
        Console.WriteLine($"Peek phần tử đầu: {q2.Peek()}");

        // TESTCASE 3
        Console.WriteLine("TESTCASE 3: QUAY CHỈ SỐ - CHƯA MỞ RỘNG");
        var q3 = new CircularQueue<string>();
        q3.Enqueue("A");
        q3.Enqueue("B");
        q3.Enqueue("C");
        q3.Enqueue("D");

        Console.Write("Dequeue 2 lần: ");
        Console.Write(q3.Dequeue() + ", ");
        Console.WriteLine(q3.Dequeue());

        q3.Enqueue("E");
        q3.Enqueue("F");
        Console.WriteLine("Đã Enqueue E, F");

        Console.Write("Dequeue 4 lần còn lại: ");
        while (q3.Count > 0)
        {
            Console.Write(q3.Dequeue() + (q3.Count > 0 ? ", " : ""));
        }
        Console.WriteLine();
        Console.WriteLine($"Capacity hiện tại là: {q3.Capacity}");

        // TESTCASE 4

        Console.WriteLine("TESTCASE 4: MỞ RỘNG KHI FRONT = 0");
        var q4 = new CircularQueue<int>();
        for (int i = 1; i <= 5; i++)
        {
            q4.Enqueue(i);
        }
        Console.WriteLine($"Đã nạp 1 -> 5. Capacity sau khi tự mở rộng: {q4.Capacity}");

        Console.Write("Dequeue 5 lần: ");
        while (q4.Count > 0)
        {
            Console.Write(q4.Dequeue() + (q4.Count > 0 ? ", " : ""));
        }
        Console.WriteLine();

        // TESTCASE 5
        Console.WriteLine("TESTCASE 5: MỞ RỘNG KHI FRONT KHÁC 0");
        var q5 = new CircularQueue<string>();
        q5.Enqueue("A");
        q5.Enqueue("B");
        q5.Enqueue("C");
        q5.Enqueue("D");

        // Rút A, B ra để front nhích lên vị trí 2
        q5.Dequeue();
        q5.Dequeue();

        // Nạp E, F, G -> G sẽ kích hoạt Resize khi mảng đang xoay vòng
        q5.Enqueue("E");
        q5.Enqueue("F");
        q5.Enqueue("G");

        Console.WriteLine($"Sau khi Enqueue G: Capacity = {q5.Capacity}");
        Console.Write("Dequeue 5 lần lấy hết: ");
        while (q5.Count > 0)
        {
            Console.Write(q5.Dequeue() + (q5.Count > 0 ? ", " : ""));
        }
        Console.WriteLine();


        // TESTCASE 6
        Console.WriteLine("TESTCASE 6: QUAY VÒNG 1000000");
        var q6 = new CircularQueue<int>();
        for (int i = 0; i < 1000000; i++)
        {
            q6.Enqueue(i);
            q6.Dequeue();
        }
        Console.WriteLine($"Hoàn thành 1000000 lần Enqueue và Dequeue!! ");
        Console.WriteLine($"Capacity: {q6.Capacity}, Count = {q6.Count}");

        // TESTCASE 7
        Console.WriteLine("TESTCASE 7: CA LỚN");
        var q7 = new CircularQueue<int>();
        var sw = Stopwatch.StartNew();

        int n = 100000;
        for (int i = 0; i < n; i++)
        {
            q7.Enqueue(i);
        }

        bool dung_thu_tu = true;
        for (int i = 0; i < n; i++)
        {
            if (q7.Dequeue() != i)
            {
                dung_thu_tu = false;
                break;
            }
        }
        sw.Stop();

        Console.WriteLine($"Lấy ra đúng thứ tự: {(dung_thu_tu ? "ĐÚNG" : "SAI")}");
        Console.WriteLine($"Capacity cuối cùng: {q7.Capacity}");
        Console.WriteLine($"Thời gian thực thi: {sw.ElapsedMilliseconds} ms");
    }
}