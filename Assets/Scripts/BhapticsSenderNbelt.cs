using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Bhaptics.Tact;

public class BhapticsSenderNbelt : MonoBehaviour
{
    private const int DurationMillis = 100;
    private const string RegisterKey = "reg";
    private IHapticPlayer _player;

    // �̷������� ���� ��Ʈ�ѵ� ���� ���� -> X���� ���ϴ� ������� ���� �ص� �˴ϴ�.
    public static void Play(int intensity, int angle, int durantion)
    {
        float y = 0.0f;
        Debug.Log("play"); 
        if (angle == 0)
        {
            float x = 0.0f; // 0.0f Front
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestFront, new PathPoint(x, y, intensity), durantion);

        }
        else if (angle == 1)
        {
            float x = 0.333f; // 0.333f
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestFront, new PathPoint(x, y, intensity), durantion);
        }
        else if (angle == 2)
        {
            float x = 1.0f; // 0.667f Front
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestBack, new PathPoint(x, y, intensity), durantion);

        }
        else if (angle == 3)
        {
            float x = 1.0f; // 1.0f Front
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestFront, new PathPoint(x, y, intensity), durantion);
        }
        else if (angle == 7)
        {
            float x = 0.667f; // 0.0f back
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestBack, new PathPoint(x, y, intensity), durantion);

        }
        else if (angle == 6)
        {
            float x = 0.333f; // 0.333f back
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestBack, new PathPoint(x, y, intensity), durantion);
        }
        else if (angle == 5)
        {
            float x = 0.0f; // 0.667f back
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestBack, new PathPoint(x, y, intensity), durantion);

        }
        else
        {
            float x = 0.667f; // 1.0f back
            HapticPlayerManager.Instance().GetHapticPlayer().Submit("t", PositionType.VestFront, new PathPoint(x, y, intensity), durantion);

        }
        

    }

    public static void Register(string filename)
    {
        var str = File.ReadAllText(filename);

        // ��ƽ ������ ��� �մϴ�. 
        HapticPlayerManager.Instance().GetHapticPlayer().RegisterTactFileStr(RegisterKey, str);
    }

    public static void PlayWithTact(int intensity,  float duRatio)
    {
        //var convertedAngle = 360 - angle; // ������ SDK���� �����ϴ� ������ ��ȯ�մϴ�.
        // var convertedIntensity = intensity * 0.01f; // ���⸦ ���� �մϴ�.
        //var durationRatio = duRatio * 1.0f; // ����Դϴ�.

        HapticPlayerManager.Instance().GetHapticPlayer().SubmitRegisteredVestRotation(RegisterKey, RegisterKey,
            new RotationOption(0, 0f), new ScaleOption(intensity, duRatio));
    }
    public static void PlayTheFile(int intensity, float duRatio)
    {

       // var convertedIntensity = intensity * 0.01f; // ���⸦ ���� �մϴ�.
      //  var durationRatio = duRatio * 1.0f; // ����Դϴ�.

        HapticPlayerManager.Instance().GetHapticPlayer().SubmitRegisteredVestRotation(RegisterKey, RegisterKey,
            new RotationOption(0, 0), new ScaleOption(intensity, duRatio));

        //HapticPlayerManager.Instance().GetHapticPlayer().SubmitRegisteredVestRotation(RegisterKey, RegisterKey,
        //    new RotationOption(convertedAngle, 0f), new ScaleOption(convertedIntensity, durationRatio));
    }

    public static void Kill()
    {
        //var str = File.ReadAllText(filename);

        // ��ƽ ������ ��� �մϴ�. 
        HapticPlayerManager.Instance().GetHapticPlayer().TurnOff();
    }
}
