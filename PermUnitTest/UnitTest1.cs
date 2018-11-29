using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PermUnitTest
{
	[TestClass]
	public class UnitTest1
	{
		string testString;

		char[] testArray;
		List<string> strResult;

		System.Text.StringBuilder builder;
		StringWriter writer;
		StringReader reader;
		Permutation.Program premutation;

		public UnitTest1()
		{
			Random rnd = new System.Random(); 
			int intResult = rnd.Next(1, 7);
			testString = Guid.NewGuid().ToString("N").Substring(0, intResult);

			Console.WriteLine("{0} / {1}", intResult.ToString(), testString);

			testArray = testString.ToCharArray();
			strResult = new List<string>();

			builder = new System.Text.StringBuilder();
			writer = new StringWriter(builder);
			premutation = new Permutation.Program();

			//結果文字列を取得
			Console.SetOut(writer);

			// テスト対象(このメソッドの中でコンソールに出力している)
			string result = premutation.PermutationPattern(testArray);
			
			// コンソール出力を読みだす
			reader = new StringReader(builder.ToString());
			writer?.Dispose();

			while (reader.Peek() > -1)
			{
				//一行ずつListに
				strResult.Add(reader.ReadLine());
			}
			reader?.Dispose();
		}

        public static long Factorial(int n)
        {
            if (n == 0)
                return 1L;
            return n * Factorial(n - 1);
        }

        public static int CountOf(string target, string str)
        {
            int count = 0;

            int index = target.IndexOf(str, 0);
            while (index != -1)
            {
                count++;
                index = target.IndexOf(str, index + str.Length);
            }

            return count;
        }

        public static string[] GetStringArray(char[] chr)
        {
            string[] retArray = new string[chr.Length];
            for (int i = 0; i < chr.Length; i++)
            {
                retArray[i] = chr[i].ToString();
            }
            return retArray;
        }

        [TestMethod]
        public void パターン数が正しいかテスト()
        {
            long samePermutation = 1;   //乗算用に初期値は1。
            string countedStr = "";		//同一文字比較用に初期値は""。

            string[] sortString = GetStringArray(testArray);
            Array.Sort(sortString);
            string str = String.Join("", sortString);
            char[] sortChar = str.ToCharArray();

            for (int i = 0; i < sortChar.Length; i++)
            {
                if (countedStr != sortChar[i].ToString())
                {
                    int strcnt = CountOf(testString, sortChar[i].ToString());
                    samePermutation *= Factorial(strcnt);
                    countedStr = sortChar[i].ToString();
                }
            }

            long permutation = Factorial(testArray.Length) / samePermutation;
            Assert.AreEqual(strResult.Count, permutation);

        }

		[TestMethod]
		public void 同じパターンが無いかテスト()
		{
			int cnt = 0;
			for (int i = 0; i < strResult.Count; i++)
			{
				for (int n = strResult.Count - 1; n > i; n--)
				{
					cnt += CountOf(strResult[i], strResult[n]);
				}
			}
			Assert.AreEqual(0, cnt);
		}

		~UnitTest1()
		{
			writer?.Dispose();
			reader?.Dispose();
		}

	}
}
