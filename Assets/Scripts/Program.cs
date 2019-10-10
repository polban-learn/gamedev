using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HiScore {
    public class Record {
        public char[] PlayerName;
        public int Score;

        public Record() {
            this.PlayerName = new char[8];
            this.Score = 0;
        }
    }
    Record[] History;
    int Pointer;

    public HiScore() {
        this.History = new Record[6];

        for (int i = 0; i < 6; i++) {
            this.History[i] = new Record();
        }
        this.Pointer = 0;
    }

    public void InitFile() {
        FileStream FScores = new FileStream("Scores.bin", FileMode.CreateNew, FileAccess.ReadWrite);
        FScores.SetLength(1024);
        FScores.Close();
    }

    public void WriteToFile() {
        FileStream FScores = new FileStream("Scores.bin", FileMode.Create, FileAccess.Write);
        BinaryWriter WrScore = new BinaryWriter(FScores);

        for (int i = 0; i < 6; i++) {
            WrScore.Write(this.History[i].PlayerName);
            WrScore.Write(this.History[i].Score);
        }
        FScores.Close();
    }

    public void ReadFile() {
        if (!File.Exists("Scores.bin")) {
            this.InitFile();
        }

        FileStream FScores = new FileStream("Scores.bin", FileMode.Open, FileAccess.Read);
        BinaryReader RdScore = new BinaryReader(FScores);

        for (int i = 0; i < 6; i++) {
            Array.Copy(RdScore.ReadChars(8), this.History[i].PlayerName, 8);
            this.History[i].Score = RdScore.ReadInt32();
        }
        FScores.Close();
    }

    private void SortHistory() {
        int Length = 6;
        for (int i = 0; i < Length - 1; i++) {
            for (int j = 0; j < Length - i - 1; j++) {
                if (this.History[j].Score < this.History[j + 1].Score) {
                    Record PTemp = this.History[j];
                    this.History[j] = this.History[j + 1];
                    this.History[j + 1] = PTemp;
                }
            }
        }
    }

    private int MinHistory() {
        int Min = this.History[2].Score;
        int Index = -1;
        for (int i = 0; i < 6; i++) {
            if (this.History[i].Score < Min) {
                Min = this.History[i].Score;
                Index = i;
            }
        }
        return Index;
    }

    public void Input(String PlayerName, int Score) {
        if (this.Pointer >= 0 && this.Pointer < 6) {
            char[] Temp = PlayerName.ToCharArray();
            Array.Clear(this.History[this.Pointer].PlayerName, 0, 8);

            if (Temp.Length > this.History[this.Pointer].PlayerName.Length) {
                Array.Copy(Temp, this.History[this.Pointer].PlayerName, 8);
            }
            else {
                Array.Copy(Temp, this.History[this.Pointer].PlayerName, Temp.Length);
            }
            
            this.History[this.Pointer++].Score = Score;
            this.SortHistory();
        }
        else {
            int Index = MinHistory();
            if (this.History[Index].Score <= Score) {
                char[] Temp = PlayerName.ToCharArray();
                Array.Clear(this.History[Index].PlayerName, 0, 8);

                if (Temp.Length > this.History[Index].PlayerName.Length) {
                    Array.Copy(Temp, this.History[Index].PlayerName, 8);
                }
                else {
                    Array.Copy(Temp, this.History[Index].PlayerName, Temp.Length);
                }

                this.History[Index].Score = Score;
                this.SortHistory();
            }
        }
    }

    public void Print() {
        for (int i = 0; i < 6; i++) {
            Console.Write(this.History[i].PlayerName);
            Console.Write("--->");
            Console.WriteLine(this.History[i].Score);
        }
    }

    public string GetName(int Index) { return new string(History[Index].PlayerName); }
    public int GetScore(int Index) { return this.History[Index].Score; }
}

namespace IOC_
{
    class Program
    {
        static void Main(string[] args)
        {
            HiScore Sc = new HiScore();
            // Sc.Input("Renol", 290);
            // Sc.Input("Rezky", 322);
            // Sc.Input("Agny", 433);
            // Sc.Input("Febriyoga Bagus Satria", 322);
            // Sc.Input("BigTable", 244);
            // Sc.Input("Iodin", 124);
            // Sc.Input("Adit", 2333);
            // Sc.WriteToFile();
            

            Sc.ReadFile();
            Sc.Print();
        }
    }
}