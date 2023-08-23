using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;

namespace soloproject
{
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }

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

    public class Equip   // 아이템 정보를 클래스로 구현 -> 배열로 다시 만들기
    {
        public bool Isequip { get; set; }
        public bool Ishave { get; set; }
        public string Info { get; }
        public string Name { get; }
        public int EquipPart { get; }

        public Equip(int equippart, bool isequip, bool ishave, string info, string name)
        {
            EquipPart = equippart;
            Isequip = isequip;
            Ishave = ishave;
            Info = info;
            Name = name;
        }

        public virtual void DisplayArmorProtect() { }

        public void DisplayEquip()
        {
            string Equip = (Isequip) ? "[E]" : "[]";
            int NameLengthRemain = (15 - Name.Length) / 2;
            int InfoLengthRemain = (30 - Info.Length) / 2;
            Console.Write("|");
            DisplayArmorProtect();
            Console.Write("|");
            Console.WriteLine();
        }

        public virtual int GetNumber()
        {
            return 0;
        }

        public void setLayout(int length, int remain, string str, bool isInt)
        {
            for (int i = 0; i < length; i++)
            {
                if (i == remain)
                {
                    Console.Write($"{str}");
                }
                else
                {
                    if (isInt)
                        Console.Write(" ");
                    else
                        Console.Write("  ");
                }
            }
        }

        public class Weapon : Equip    // 무기 세팅
        {
            public int Attack { get; }
            public Weapon(int equippart, bool isequip, bool ishave, string info, string name, int attack) : base(equippart, isequip, ishave, info, name)
            {
                Attack = attack;
            }

            public override void DisplayArmorProtect()
            {
                int AttackLengthRemain = (6 - Attack.ToString().Length) / 2;
                setLayout(6 - Attack.ToString().Length, AttackLengthRemain, $"공격력 : +{Attack}", true);
            }

            public override int GetNumber()
            {
                return Attack;
            }
        }
        
    }

    
    public class Armor : Equip  // 방어구 세팅
    {
        public int Protect { get; }
        public Armor(int equippart, bool isequip, bool ishave, string info, string name, int protect) : base(equippart, isequip, ishave, info, name)
        {
            Protect = protect;
        }

        public override void DisplayArmorProtect()
        {
            int ProtectLengthRamain = (6 - Protect.ToString().Length) / 2;
            setLayout(6 - Protect.ToString().Length, ProtectLengthRamain, $"방어력 : +{Protect}", true);
        }

        public override int GetNumber()
        {
            return Protect;
        }

        internal class Program
        {
            private static List<string> data = new List<string>();
            private static Character player;
            private static List<Equip> equip = new List<Equip>();
            static void EquipDataSetting()
            {
                Weapon bronzeSword = new Weapon(1, true, true, "쉽게 볼 수 있는 낡은 검입니다.", "낡은 검", 5);
                Weapon knightSword = new Weapon(1, true, false, "잘 벼려진 평범한 검입니다.", "평범한 검", 15);
                Weapon heavySword = new Weapon(1, true, false, "무겁지만 강력한 검입니다.", "대검", 30);

                Armor bronzeArmor = new Armor(2, true, true, "무쇠로 만들어져 튼튼한 갑옷입니다.", "무쇠갑옷", 5);
                Armor silverArmor = new Armor(2, true, false, "은으로 만들어져 더 튼튼한 갑옷입니다.", "은갑옷", 15);
                Armor goldArmor = new Armor(2, true, false, "금으로 만들어져 가장 튼튼한 갑옷입니다.", "금갑옷", 30);

                equip.Add(bronzeArmor);
                equip.Add(silverArmor);
                equip.Add(goldArmor);
                equip.Add(heavySword);
                equip.Add(knightSword);
                equip.Add(heavySword);
            }

            static int Checknum(int equippart)
            {
                int num = 0;
                for (int i = 0; i < equip.Count; i++)
                {
                    if (equip[i].Isequip && equip[i].EquipPart == equippart)
                    {
                        num += equip[i].GetNumber();
                    }
                }
                return num;
            }

            static void Main(string[] args)
            {
                GameDataSetting();
                EquipDataSetting();
                DisplayGameIntro();
            }

            static void GameDataSetting()
            {
                player = new Character("bastian", "전사", 1, 10 + Checknum(1), 5 + Checknum(2), 100, 1500);
            }

            static void EquipPlayerDataSetting(int num, int equippart, bool istake)
            {
                // 공격력
                if (equippart == 1)
                {
                    if (istake)
                        player.Atk += num;
                    else
                        player.Atk -= num;
                }

                // 방어력
                if (equippart == 2)
                {
                    if (istake)
                        player.Def += num;
                    else
                        player.Def -= num;
                }
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
                }
            }

            static void DisplayMyInfo()
            {
                Console.Clear();
                // 2. 상태보기
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("상태보기");
                Console.ResetColor();

                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"LV.{player.Level}");
                Console.WriteLine($"{player.Name}({player.Job})");

                int plusAtk = Checknum(1);
                int plusDef = Checknum(2);
                Console.Write($"공격력 : {player.Atk} ");
                if (plusAtk == 0)
                {
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine($"({Checknum(1)})");
                }
                Console.Write($"방어력 : {player.Def} ");
                if (plusAtk == 0)
                {
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine($"({Checknum(2)})");
                }
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
                Console.Clear();
                // 3. 인벤토리

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("인벤토리");
                Console.ResetColor();

                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < equip.Count; i++)
                {
                    if (equip[i].Ishave)
                    {
                        Console.Write($"- ");
                        equip[i].DisplayEquip();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = CheckValidInput(0, 1);
                switch (input)
                {
                    case 1:
                        DisplayEquipManagement();
                        break;

                    case 0:
                        DisplayGameIntro();
                        break;
                }


            }


            static void DisplayEquipManagement()
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("인벤토리 - 장착관리");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Dictionary<int, int> dic = new Dictionary<int, int>();
                int num = 0;
                for (int i = 0; i < equip.Count; i++)
                {
                    if (equip[i].Ishave)
                    {
                        num++;
                        Console.Write($"- {num}. ");
                        equip[i].DisplayEquip();
                        dic.Add(num, i);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = CheckValidInput(0, num);

                
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
                    //잘못된 값 입력시 빨간색 문구가 뜨도록 변경
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                }
            }

        }
    }
}


