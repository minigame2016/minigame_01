using UnityEngine;
using System.Collections;

public class TableNum {
    public static int PlayerUpItemNum_1 = 3;//上面存储的数量，随分数/时间改变
    public static int PlayerUpItemNum_2 = 4;//上面存储的数量，随分数/时间改变
    public static int PlayerUpItemNum_3 = 5;//上面存储的数量，随分数/时间改变

    public static int GradeWeights_1 = 100;//分数权值
    public static int GradeWeights_2 = 150;//分数权值
    public static int GradeWeights_3 = 200;//分数权值

    public static int UpSpeedGradeNode_1 = 1000;//达到多少分增加速度
    public static int UpSpeedGradeNode_2 = 2000;//达到多少分增加速度   同时增加框4
    public static int UpSpeedGradeNode_3 = 3000;//达到多少分增加速度
    public static int UpSpeedGradeNode_4 = 4000;//达到多少分增加速度   同时增加框5
    public static int UpSpeedGradeNode_5 = 5000;//达到多少分增加速度
    public static int UpSpeedGradeNode_6 = 6000;//达到多少分增加速度，速度UpSpeedScale_1
    public static int UpSpeedGradeNode_7 = 7000;//达到多少分增加速度，速度UpSpeedScale_2
    public static int UpSpeedGradeNode_8 = 8000;//达到多少分增加速度，速度UpSpeedScale_3
    public static int UpSpeedGradeNode_9 = 9000;//达到多少分增加速度，速度UpSpeedScale_6
    public static int UpSpeedGradeNode_10 = 11000;//达到多少分增加速度，速度UpSpeedScale_7
    public static int UpSpeedGradeNode_11 = 13000;//达到多少分增加速度，速度UpSpeedScale_8
    public static int UpSpeedGradeNode_12 = 15000;//达到多少分增加速度，速度UpSpeedScale_9
    public static int UpSpeedGradeNode_13 = 17000;//达到多少分增加速度，速度UpSpeedScale_10
    public static int UpSpeedGradeNode_14 = 20000;//达到多少分增加速度，速度UpSpeedScale_11

    public static float UpSpeedScale_1 = 1.2f;//速度增加倍数
    public static float UpSpeedScale_2 = 1.4f;//速度增加倍数
    public static float UpSpeedScale_3 = 1.6f;//速度增加倍数
    public static float UpSpeedScale_4 = 1.2f;//速度增加倍数
    public static float UpSpeedScale_5 = 1f;//速度增加倍数
    public static float UpSpeedScale_6 = 1.8f;//速度增加倍数
    public static float UpSpeedScale_7 = 1.9f;//速度增加倍数
    public static float UpSpeedScale_8 = 2.0f;//速度增加倍数
    public static float UpSpeedScale_9 = 2.1f;//速度增加倍数
    public static float UpSpeedScale_10 = 2.3f;//速度增加倍数
    public static float UpSpeedScale_11 = 2.5f;//速度增加倍数

    public static float BreakDelayTime = 0.5f;//点击消除延迟音效

    public static int RamdomCounter_1 = 3;//三个以内没有重复
    public static int RamdomCounter_2 = 4;//三个以内没有重复
    public static int RamdomCounter_3 = 5;//三个以内没有重复

    public static double DeadLine = -0.85;

    public static float HpSpeed = 0.4f;//血包下降速度
    public static int HpCreatTime = 30;//多场时间产生血包

    public static int Hp = 3;

    public static int ScoreNode_1 = 0;
    public static int ScoreNode_2 = 2000;
    public static int ScoreNode_3 = 8000;
    public static int ScoreNode_4 = 12000;

    public static float ReverseSpeed = 0.1f;//反道具下降的速度
    public static int ReverseCreateTime = 10;//多场时间产生一个反道具
    public static int ReverseAddSpeed = 8;//反道具是速度增加多少
    public static float ReverseAddTime = 1.5f;//反道具生效多长时间
}
