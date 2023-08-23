using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;

namespace soloproject
{
    internal class Program
    {
        private static Character player;
        private static Item item;
        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("bastian", "전사", 1, 10, 5, 100, 1500);
        }
        static void DisplayGameIntro()
        {
            Console.Clear();
            // 1. 게임 시작 화면
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            // 숫자를 입력시 실행되는 창 설정.
            // 1은 상태보기이므로 DisplayMyInfo
            // 2는 인벤토리이므로 DisplayInventory

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();
            // 2. 상태보기
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"LV.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 : {player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold}");
            Console.WriteLine();
            Console.WriteLine("0 : 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            //아이템 설정
            Item first = new Item();
            first.Name = "무쇠갑옷";
            first.Stat = "방어력";
            first.Increase = 5;
            first.Info = "무쇠로 만들어져 튼튼한 갑옷입니다.";

            Item second = new Item();
            second.Name = "낡은 검";
            second.Stat = "공격력";
            second.Increase = 2;
            second.Info = "쉽게 볼 수 있는 낡은 검 입니다.";

            Console.Clear();
            // 3. 인벤토리
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            first.PrintInfo();
            second.PrintInfo();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");




        }

        static int CheckValidInput(int min, int max)   // 입력받을때 input 값이 맞는 값인지 판단하는 부분, 맞으면 출력하고 틀리면 다시 while문으로 돌리기 위해서 필요한 부분!
        {
            while (true)
            {
                bool parseSuccess = int.TryParse(Console.ReadLine(), out var ret);   // var키워드 , 형변환 TryParse 함수 사용
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");

            }
        }

    }
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }

    }

    public class Item
    {
        public string Name;
        public string Stat;
        public int Increase;
        public string Info;

        public void PrintInfo()
        {
            Console.WriteLine("- " + Name + " | " + Stat + " +" + Increase + " | " + Info);
        }
    }



}
