using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトル、ステージセレクトに使う汎用性のあるものの定数宣言リスト
/// 制作者:番場宥輝
/// </summary>
public static class Menu {
    
    //  タイトルメニュー

    public const int START = 0;
    public const int CONFIG = 1;
    public const int CREDIT = 2;
    public const int END = 3;

    //  コンフィグ

    public const int CLOSE = 0;
    public const int SOUND = 1;
    public const int BUTTON = 2;
    public const int DATA = 3;
     
    public const int TITLE = 3;

    //キー

    public const int A = 0;
    public const int X = 1;
    public const int Y = 2;
    public const int B = 3;

    //音量

    public const int MAX_VOL = 10;
    public const int MIN_VOL = 0;

    //フェード間隔
    public const int FADE = 40;

    //キーのウエイト
    public const int KEY_WAIT = 15;

    //ステージ
    public const int MONSTER = 0;
    public const int SEA = 1;
    public const int GHOST = 2;
    public const int JAPANESE = 3;
    public const int GOD = 4;

    public const int TOWER_NUM = 5;

    //フロア
    public const int FLOOR1 = 0;
    public const int FLOOR2 = 1;
    public const int FLOOR3 = 2;
    public const int FLOOR4 = 3;
    public const int FLOOR5 = 4;
    public const int FLOOR6 = 5;

    public const int FLOOR_NUM = 6;
}
