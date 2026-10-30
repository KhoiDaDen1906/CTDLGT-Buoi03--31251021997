using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics; // Dùng thư viện này để dùng stopwatch cho testcase 9-10.
internal class Dem_ngoac
{
    private static void Main123(string[] args)
    {
        // Khai báo Stack có chỉ đích danh ký tự và vị trí của nó
        Stack<(char ch , int pos)> st = new Stack<(char ch, int pos)> ();

        // Cho người dùng nhập vào chuỗi ký tự.
        System.Console.WriteLine("Nhập vào 1 chuỗi các ký tự: ");
        string s = Console.ReadLine() ?? "(a+b) = {[a-b] x [a:b]}";

        // Dùng vòng lặp duyệt qua từng ký tự trong chuỗi và xem nếu có những cái mở ngoặc hay không
        for (int i = 0; i < s.Length ; i++)
        {
            // Nếu có mở ngoặc thì thêm chúng vào Stack.
            if (s[i] == '(' || s[i] == '{' || s[i] == '[' )
            {
                st.Push ((s[i],i));
            }
            // Xét tiếp điều kiện nếu như tìm thấy các ngoặc đóng thì sẽ có những trường hợp bị lỗi sau:
            else if (s[i] == ')' || s[i] == '}' || s[i] == ']')
            {
                // Nếu tìm thấy một ngoặc đóng nào trong khi Stack vẫn chưa có phần tử nào - tức là
                // trước đó chưa có dấu mở ngoặc nào.
                if (st.Count == 0)
                {
                    // Báo lỗi Đóng thừa ngoặc và dừng chương trình luôn vì đã thấy lỗi.
                    System.Console.WriteLine($"SAI tai vi tri {i}");
                    return;
                }
                // Tiếp theo xem phần tử top là gì để đi đối chiếu với lỗi đóng sai ngoặc
                // Trong stack được khai báo bởi char ch và int pos nên sử dụng var, nếu không sẽ lỗi biên dịch.
                var top = st.Peek();

                // Điều kiện để các ngoặc bị đóng sai thứ tự, kiểm tra bằng giá trị top của stack vừa khai báo
                if ((s[i] == ')' && top.ch != '(')||
                    (s[i] == '}' && top.ch != '{')||
                    (s[i] == ']' && top.ch != '['))
                {
                    // In lỗi ra màn hình và dừng luôn chương trình.
                    System.Console.WriteLine($"SAI tai vi tri {i}");
                    return;
                }
                // Nếu vượt qua được đến bước thì không còn lỗi, xong một cặp ngoặc
                // Nên ta loại bỏ ngoặc mở đang ở vị trí top ra và tiếp tục xét cái tiếp theo
                st.Pop();
            }
        }
        // Sau khi đã duyệt hết chuỗi, nếu như trong stack vẫn còn phần tử, tức là lỗi chưa đóng ngoặc!
        // Ta đi khai báo lỗi ở vị trí thấp nhất của stack.
        if (st.Count != 0)
        {
            // Khai báo chỉ số là -1 để nếu như không có bất kì phần tử nào thì quy ước là "Không có gì" thay vì sập chương trình
            int bottom_pos = -1;
            // Tận dụng tích chất duyệt giá trị từ top -> bottom của Stack, sử dụng foreach để duyệt
            foreach(var item in st)
            {
                // nó sẽ gán giá trị của phần tử cuối cùng (nằm dưới đáy) của stack.
                bottom_pos = item.pos;
            }
            // In kết quả ra màn hình và dừng luôn chương trình.
            System.Console.WriteLine($"SAI tai vi tri {bottom_pos}");
            return;
        }
        System.Console.WriteLine("HOP LE");

        // testcase 9 và 10
        ChayTestcase9Va10();
    }
    //Hàm này dùng cho TestCase 9 và 10.
    private static void ChayTestcase9Va10()
    {
        Console.WriteLine("ĐO THỜI GIAN THỰC THI CỦA CHƯƠNG TRÌNH ĐỐI VỚI TESTCASE LỚN");

        // CA 9: 500.000 dấu '(' rồi 500.000 dấu ')'
        Console.WriteLine("\n TESTCASE 9: 500.000 dấu '(' rồi 500.000 dấu ')'");
        string s9 = new string('(', 500000) + new string(')', 500000);
        
        Stopwatch sw9 = Stopwatch.StartNew(); // Bấm giờ
        Stack<(char ch, int pos)> st9 = new Stack<(char ch, int pos)>();
        bool hopLe9 = true;

        for (int i = 0; i < s9.Length; i++)
        {
            if (s9[i] == '(')
            {
                st9.Push((s9[i], i));
            }
            else if (s9[i] == ')')
            {
                if (st9.Count == 0) { hopLe9 = false; break; }
                st9.Pop();
            }
        }
        if (st9.Count != 0) hopLe9 = false;
        sw9.Stop(); // Dừng giờ

        Console.WriteLine($"Kết quả : {(hopLe9 ? "HOP LE" : "SAI")}");
        Console.WriteLine($"Thời gian: {sw9.ElapsedMilliseconds} ms => ĐẠT (Kỳ vọng: < 1000 ms)");

        // CA 10: 1.000.000 dấu '(' (Stack sâu nhất) 
        Console.WriteLine("\n TESTCASE 10: 1.000.000 dấu '(' (Stack sâu nhất)");
        string s10 = new string('(', 1000000);
        
        Stopwatch sw10 = Stopwatch.StartNew(); // Bấm giờ
        Stack<(char ch, int pos)> st10 = new Stack<(char ch, int pos)>();

        for (int i = 0; i < s10.Length; i++)
        {
            st10.Push((s10[i], i));
        }

        int bottom10 = -1;
        foreach (var item in st10)
        {
            bottom10 = item.pos;
        }
        sw10.Stop(); // Dừng giờ

        Console.WriteLine($"Kết quả : SAI tai vi tri {bottom10}");
        Console.WriteLine($"Thời gian: {sw10.ElapsedMilliseconds} ms => ĐẠT (Kỳ vọng: < 1000 ms)");
    }
}