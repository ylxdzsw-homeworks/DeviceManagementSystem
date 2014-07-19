using System;
using System.Collections.Generic;
using System.Web;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using System.Drawing;

/// <summary>
/// Summary description for QRtool
/// </summary>
public class QRtool
{
    public static String makeQRimg(String content)
    {
        BitMatrix b = new BitMatrix(2);
        Bitmap img = b.ToBitmap(BarcodeFormat.QR_CODE,content);
        string filename = @"C:\Users\Administrator\Desktop\fuck-project\WebTry\DevicemanagementSystem\Image\QRimgs\QR" + DateTime.Now.Ticks.ToString() + ".jpg";
        img.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
        Debug.log("生成了二维码图片"+filename+" (QRtool.cs - 2175)");
        return filename.Substring(filename.IndexOf("\\Image"));
    }
}
