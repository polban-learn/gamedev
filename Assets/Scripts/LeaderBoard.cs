using System;
using System.IO;

public class LeaderBoard {
    public class Record {
        public bool fillUp;
        public char[] playerName;
        public uint score;

        public Record() {
            this.fillUp = false;
            this.playerName = new char[11];
            this.score = 0;
        }
    }
    public Record[] playerRecord;
    private int pointer;

    public LeaderBoard() {
        this.playerRecord = new Record[8];
        for (int i = 0; i < 8; i++) {
            this.playerRecord[i] = new Record();
        }
        this.pointer = 0;
    }

    public void initFile() {
        FileStream fLeaderBoard = new FileStream("LeaderBoard.bin",
                                    FileMode.CreateNew, FileAccess.ReadWrite);
        fLeaderBoard.SetLength(1024);
        fLeaderBoard.Close();
    }

    public void writeToFile() {
        FileStream fLeaderBoard = new FileStream("LeaderBoard.bin",
                                    FileMode.Create, FileAccess.Write);
        BinaryWriter fPlayerRecord = new BinaryWriter(fLeaderBoard);
        for (int i = 0; i < 8; i++) {
            fPlayerRecord.Write(this.playerRecord[i].fillUp);
            fPlayerRecord.Write(this.playerRecord[i].playerName);
            fPlayerRecord.Write(this.playerRecord[i].score);
        }
        fPlayerRecord.Write(this.pointer);
        fLeaderBoard.Close();
    }

    public void readFile() {
        if (!File.Exists("LeaderBoard.bin")) {
            this.initFile();
        }

        FileStream fLeaderBoard = new FileStream("LeaderBoard.bin",
                                    FileMode.Open, FileAccess.Read);
        BinaryReader fPlayerRecord = new BinaryReader(fLeaderBoard);
        for (int i = 0; i < 8; i++) {
            this.playerRecord[i].fillUp = fPlayerRecord.ReadBoolean();
            Array.Copy(fPlayerRecord.ReadChars(11), this.playerRecord[i].playerName, 11);
            this.playerRecord[i].score = fPlayerRecord.ReadUInt32();
        }
        this.pointer = fPlayerRecord.ReadInt32();
        fLeaderBoard.Close();
    }

    private void sortLeaderBoard() {
        for (int i = 0; i < this.pointer - 1; i++) {
            for (int j = 0; j < this.pointer - i - 1; j++) {
                if (this.playerRecord[j].score < this.playerRecord[j + 1].score) {
                    Record pTemp = this.playerRecord[j];
                    this.playerRecord[j] = this.playerRecord[j + 1];
                    this.playerRecord[j + 1] = pTemp;
                }
            }
        }
    }

    private int minRecord() {
        var min = this.playerRecord[0].score;
        int index = -1;

        for (int i = 0; i < this.pointer; i++) {
            if (this.playerRecord[i].score < min) {
                min = this.playerRecord[i].score;
                index = i;
            }
        }
        return index;
    }

    public void input(string playerName, uint score) {
        if (this.pointer >= 0 && this.pointer < 8) {
            char[] temp = playerName.ToCharArray();
            Array.Clear(this.playerRecord[this.pointer].playerName, 0, 11);
            this.playerRecord[this.pointer].fillUp = true;

            if (temp.Length > this.playerRecord[this.pointer].playerName.Length) {
                Array.Copy(temp, this.playerRecord[this.pointer].playerName, 11);
            }
            else {
                Array.Copy(temp, this.playerRecord[this.pointer].playerName, temp.Length);
            }
            
            this.playerRecord[this.pointer++].score = score;
            this.sortLeaderBoard();
        }
        else {
            int index = this.minRecord();
            this.playerRecord[index].fillUp = true;

            if (this.playerRecord[index].score <= score) {
                char[] temp = playerName.ToCharArray();
                Array.Clear(this.playerRecord[index].playerName, 0, 11);

                if (temp.Length > this.playerRecord[index].playerName.Length) {
                    Array.Copy(temp, this.playerRecord[index].playerName, 8);
                }
                else {
                    Array.Copy(temp, this.playerRecord[index].playerName, temp.Length);
                }

                this.playerRecord[index].score = score;
                this.sortLeaderBoard();
            }
        }
    }

    public void print() {
        for (int i = 0; i < 8; i++) {
            if (this.playerRecord[i].fillUp) {
                Console.Write(this.playerRecord[i].playerName);
                Console.Write("--->");
                Console.WriteLine(this.playerRecord[i].score);
            }
        }
    }

    private int charArrayLength(char[] charArray) {
        int length = 0;
        for (int i = 0; i < charArray.Length; i++) {
            if (charArray[i] != '\0') {
                length++;
            }
        }
        return length;
    }

    public string getName(int index) {
        int length = this.charArrayLength(this.playerRecord[index].playerName);
        char[] newName = new char[length];
        Array.Copy(this.playerRecord[index].playerName, newName, length);
        return new string(newName);
    }
    public uint getScore(int index) { return this.playerRecord[index].score; }
}