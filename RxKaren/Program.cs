using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxKaren
{
    /// <summary>
    /// mainから奈緒に投げる
    /// </summary>
    class RxTest
    {
        /// <summary>
        /// メインエントリ
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // インスタンス生成
            Nao nao = new Nao();

            // 奈緒にスタートしてもらうことにした
            nao.Start();
        }
    }
    /// <summary>
    /// 奈緒
    /// </summary>
    public class Nao
    {
        /// <summary>
        /// すたーとする奴
        /// </summary>
        public void Start()
        {
            // 名前の入力
            Console.WriteLine("名前を入力");
            MyName = Console.ReadLine();

            // Rxうんぬん使って、WriteConsoleに一秒ごとにイベント投げるか
            var task = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(x => WriteConsole());

            Console.WriteLine("ぷれす なんちゃら えんたー");
            Console.ReadLine();

            //使い終わったスレッドは削除しとく
            task.Dispose();

        }

        /// <summary>
        /// コンソールに書くやつ
        /// </summary>
        public void WriteConsole()
        {
            // 今の時間をセット
            NowTime = DateTime.Now;

            // if で切ったところをイベントとして投げるといい感じだったかも
            // 秒数が0だったら凄いエモートするか
            if (NowTime.Second == 0)
                Console.WriteLine($"{NowTime.ToLongTimeString()} / { MyName }、{ SuperEmotes.OrderBy(y => Guid.NewGuid()).First()} ");

            else
                // えもーとする
                // .First()が抜けてました。
                // .Take(1)で 取った気になるじゃないですか！ 
                // 1発じゃ気づきにくいですよね（　＾ω＾）・・・
                Console.WriteLine($"{NowTime.ToLongTimeString()} / { MyName }、{ Emotes.OrderBy(y => Guid.NewGuid()).First()} ");

        }

        /// <summary>
        /// 入力した名前
        /// </summary>
        public string MyName { get; set; }

        /// <summary>
        /// 今の時間
        /// </summary>
        public DateTime NowTime { get; set; }

        /// <summary>
        /// エモート
        /// 各リーダの奴
        /// </summary>
        public IEnumerable<string> Emotes = new[]
        {
            "お見事ですね！",
            "なかなかやるな",
            "この過酷あふれる世界に救いを・・・",
            "よろしくねっ！",
            "すごい！すごい！",
            "私を愉しませてくれたまえッ！",
            "私の魔法にひれ伏しなさいッ！"
        };

        /// <summary>
        /// スーパなエモート
        /// 初期メンバ以外のエモート
        /// </summary>
        public IEnumerable<string> SuperEmotes = new[]
        {
            "ヴァンピィちゃんはさいきょーですし！",
            "見事だ",
            "我がサイコパワーにひれ伏せぇ！"
        };
    }

}
