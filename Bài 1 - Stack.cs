internal class Stack1
{
    // Đề bài: Viết chương trình ghi ra số ngày tiếp theo có nhiệt độ cao hơn nhiệt độ ngày được xét. (sử dụng stack)
    private static void Main12(string[] args)
    {
        // Tạo mảng chứa các nhiệt độ
       int [] arr = new int [] {73, 74, 75, 71, 69, 72, 76, 73} ;
       // Tạo stack để chứa các index 
       Stack<int> stack = new Stack<int> ();
       // Tạo mảng chứa kết quả
       int [] ans = new int [arr.Length];

       for (int i = 0; i < arr.Length ; i++)
        {
            while (stack.Count > 0 && arr[i] > arr[stack.Peek()])
            {
                int preDay = stack.Pop();
                ans[preDay] = i - preDay;
            }
            stack.Push(i);
        }

        Console.WriteLine("So ngay cho cua tung ngay la:");
        Console.WriteLine(string.Join(" ", ans));
    }
}