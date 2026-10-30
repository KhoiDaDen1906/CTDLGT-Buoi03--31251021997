internal class Queue1
{

    static int[] Counting_circle(int[] a, int step)
    {
    int n = a.Length;
    int[] ans = new int[n];
    Queue<int> queue = new Queue<int>();

    // Cho n người xếp hàng ban đầu 
    for (int i = 1; i <= n; i++)
    {
        queue.Enqueue(i);
    }

    // Lần lượt loại từng người cho đến khi đủ n người vào mảng ans
    for (int i = 0; i < n; i++) 
    {
        // Nhảy (step - 1) người bằng cách bốc đầu nhét đuôi
        for (int k = 0; k < step - 1; k++)
        {
            int left = queue.Dequeue();
            queue.Enqueue(left);
        }

        // Người thứ step chính là người đứng đầu hàng hiện tại -> loại ra khỏi vòng
        ans[i] = queue.Dequeue();
    }

        return ans;
    }
}