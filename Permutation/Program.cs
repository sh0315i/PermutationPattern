using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Permutation
{
	public class Program
	{
		List<string> strResultList= new List<string> ();

		public static int minInput = 1;
		public static int maxInput = 7;

		public static void Main()
		{
			var program = new Program();

			Console.WriteLine("1文字～7文字の文字列を入力してください。");
			string inputString = Console.ReadLine();
			Console.WriteLine();

			while (inputString.Count() < minInput || inputString.Count() > maxInput)
			{
				Console.WriteLine("1文字～7文字の文字列ではありません。" + Environment.NewLine + "1文字～7文字の文字列を入力してください。");
				inputString　= Console.ReadLine();
				Console.WriteLine();
			}

			Console.WriteLine("結果:");
			program.PermutationPattern(inputString.ToCharArray());
			Console.WriteLine("出力が完了しました。任意のキーを押してください");
			Console.ReadKey();
		}

		// 順列を求める
		public string PermutationPattern(char[] t, int n = -1) //配列比較用にn = -1。再帰処理を考慮してデフォルト引数。
		{
			int i, k;
			char chr = ' ';

			if (n == t.Length - 1)
			{
				string result = new string(t);
				Output(result);
				strResultList.Add(result);
			}
			k = n + 1;
			for (i = n + 1; i < t.Length; i++)
			{
				chr = t[k];
				t[k] = t[i];
				t[i] = chr;
				PermutationPattern(t, k);
				t[i] = t[k];
				t[k] = chr;
			}
            return new string(t);
        }

		// 順列を List
		public void Output(string t)
		{
			if (!strResultList.Any(n => n == t))
			{
				Console.WriteLine(t);
			}
		}
	}
}
