using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;
record Customer(int id, long a, long s);
internal class Service
{
    static void Service_System(int k , List<(long a , long s)> listKH)
    {
        // Khởi tạo Hàng đợi chứa thông tin của khách hàng bao gồm id, thời điểm đến và thời gian được phục vụ
        Queue<Customer> queue = new Queue<Customer> ();

        // Khởi tạo trạng thái k quầy. đến k + 1 để đánh số quầy cho tiện, thay vì là quầy 0 thì giờ là quầy 1.
        // available_counter là mảng chứa thời điểm quầy thứ k xong việc và rảnh.
        long[] available_counter = new long [k + 1];

        // Cho toàn bộ khách hàng vào hàng đợi theo id của mỗi người (id được gán từ 1 -> độ rộng của ListKH)
        // Dùng vòng lặp để ghi chép lại thông tin của khách hàng
        for (int i = 0 ; i < listKH.Count ; i++)
        {
            // Lưu id, thời gian đến, thời gian được phục vụ của mỗi khách hàng đến.
            queue.Enqueue(new Customer(i+1, listKH[i].a, listKH[i].s));
        } 

        // Khởi tạo các biến thống kê cho đối tượng khách hàng.
        long total_Wait = 0;
        long max_Wait = 0;
        long finish_All = 0;

        // Khởi tạo vòng lặp điều phối cho đến khi nào không còn khách hàng trong danh sách nữa.
        while(queue.Count > 0)
        {
            // Gán mỗi khách hàng vào một biến để dễ làm việc. Rút khách hàng khỏi danh sách và gán vào biến
            Customer c = queue.Dequeue();
            
            // Giả định rằng quầy 1 đang là quầy rảnh nhất hiện tại
            int best_counter = 1;
            long min_Time = available_counter[1];
            
            // Kiểm tra xem nếu có thêm quầy q nào đó đó rảnh hơn quầy 1 hiện tại 
            // Xét từ quầy 2 đến quầy k
            for (int q = 2; q <= k ; q ++)
            {
                // Nếu thấy quầy q rảnh hơn hẳn quầy rảnh nhất hiện tại
                if (available_counter[q] < min_Time)
                {
                    //Cập nhật lại thời gian rảnh sớm nhất.
                    min_Time = available_counter[q];
            
                    //Ghi nhận quầy q là quầy rảnh nhất hiện tại.
                    best_counter = q;
                }
            }
            // Tới đây có 2 tình huống xảy ra khi ta tính thời điểm khách hàng bắt đầu được phục vụ.
            // TH1: Quầy rảnh nhưng phải chờ khách tới mới phục vụ
            // TH2: Khách tới nhưng quầy bận, chưa phục vụ kịp.
            // ==> Cả 2 thời điểm đều quy về việc ta sẽ lấy thời điểm LỚN HƠN giữa lúc khách đến (c.a) và lúc quầy rảnh.
            // Thời điểm b là thời điểm mà khách hàng bắt đầu được phục vụ.
            long b = Math.Max(c.a , available_counter[best_counter]);

            //Thời gian khách hàng phải chờ để được phục vụ.
            long w = b - c.a;

            // Thời điểm mà khách hàng được phục vụ xong.
            long e = b + c.s;

            // Cập nhật lại thời điểm mà quầy rảnh và sẵn sàng để phục vụ tiếp tục. Đó chính là thời điểm e
            // khi vừa phục xong khách hàng trước đó.
            available_counter[best_counter] = e;

            // Tính tổng thời gian chờ của các khách hàng.
            total_Wait += w;

            // Tìm thời gian chờ lâu nhất trong số những khách hàng được phục vụ.
            if (w > max_Wait) max_Wait = w;

            // Tìm thời điểm hoàn thành toàn bộ tất cả giao dịch => Kiểm tra mốc e có phải là thời điểm muộn nhất hay không.
            if (e > finish_All) finish_All = e;

            // In thông tin của khách hàng vừa được phục vụ. Thêm vào điều kiện để phục vụ cho testcase 6.
            Console.WriteLine($"KH {c.id}: den {c.a}, quay {best_counter}, bat dau {b}, cho {w}, xong {e}");
        }

        // Kiểm tra xem danh sách khách hàng có trống hay không để chia không bị lỗi chia cho 0.
        if (listKH.Count == 0)
        {
            System.Console.WriteLine("Lỗi danh sách khách hàng rỗng!");
            return;
        }

        // Tính thời gian chờ trung bình của toàn bộ khách hàng.
        double average_Wait = (double) total_Wait / listKH.Count;

        // In ra các chỉ số thống kê về thời gian chờ trung bình, thời gian chờ lâu nhất và thời gian hoàn thành toàn bộ giao dịch
        Console.WriteLine($"Cho trung binh: {average_Wait.ToString("F2", CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Cho lau nhat: {max_Wait}");
        Console.WriteLine($"Hoan thanh luc: {finish_All}");
    }
    private static void Main_Service(string[] args)
    {
        // Chạy testcase 1:
        System.Console.WriteLine("TESTCASE 1: "); 
        int k1 = 1; 
        var list1 = new List<(long a, long s)>
        {
            (0, 5),
            (1, 3),
            (2, 4)
        };
        Service_System (k1 , list1);

        // Chạy testcase 2: 
        System.Console.WriteLine("TESTCASE 2: "); 
        int k2 = 2; 
        var list2 = new List<(long a, long s)>
        {
            (0, 5),
            (1, 3),
            (2, 4)
        };
        Service_System (k2 , list2);

        // Chạy testcase 3:
        System.Console.WriteLine("TESTCASE 3: "); 
        int k3 = 2; 
        var list3 = new List<(long a, long s)>
        {
            (0, 3),
            (0, 3),
            (0, 3) 
        };
        Service_System (k3 , list3);

        // Chạy testcase 4:
        System.Console.WriteLine("TESTCASE 4: "); 
        int k4 = 1; 
        var list4 = new List<(long a, long s)>
        {
            (0, 2),
            (10, 3)
        };
        Service_System (k4 , list4);

        // Chạy testcase 5:
        System.Console.WriteLine("TESTCASE 5: "); 
        int k5 = 3; 
        var list5 = new List<(long a, long s)>
        {
            (4, 6)
        };
        Service_System (k5 , list5);

        // Chạy testcase 6: Ca lớn
        Console.WriteLine("TESTCASE 6: Ca lớn");
        int n6 = 100000;
        int k6 = 10;
        var list6 = new List<(long a, long s)>(n6);

        // Sinh dữ liệu mẫu cho 100.000 khách (ví dụ khách đến liên tục mỗi giây a = i, thời gian phục vụ s từ 1 đến 10)
        // Cố định seed 42 để kết quả các lần chạy đồng nhất
        Random rand = new Random(42); 
        for (int i = 0; i < n6; i++)
        {
            // Thời điểm đến tăng dần
            long a = i;                 
            // Thời gian xử lý từ 1 đến 10 giây
            long s = rand.Next(1, 11);  
            list6.Add((a, s));
        }

        // Bấm giờ đo hiệu năng thuật toán
        Stopwatch sw = Stopwatch.StartNew();

        Service_System(k6, list6);

        sw.Stop();
        Console.WriteLine($"Thời gian thực thi: {sw.ElapsedMilliseconds} ms (Kỳ vọng: < 1000 ms) => {(sw.ElapsedMilliseconds < 1000 ? "ĐẠT" : "KHÔNG ĐẠT")}");
    }
}