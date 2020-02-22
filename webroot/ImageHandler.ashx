<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Drawing.Drawing2D;
using System.Web;
using System.Drawing;
using System.IO;
using Synion.Util.Manager;


public class ImageHandler : IHttpHandler {

    public static string ApplicationPath
    {
        get { return ((HttpContext.Current.Request.ApplicationPath == "/") ? "" : HttpContext.Current.Request.ApplicationPath); }
    }

    public void ProcessRequest(HttpContext context)
    {
        string imgUrl = context.Request.QueryString["url"];
        string dimension = context.Request.QueryString["dim"];
        string webSiteId = context.Request.QueryString["WebSiteId"];
        string width = context.Request.QueryString["width"];
        string cropto = context.Request.QueryString["cropto"];
        int cornerRadius = 0;

        string cornerColor = "gray";
        if (context.Request.QueryString["cornerradius"] != null)
        {
            Int32.TryParse(context.Request.QueryString["cornerradius"].ToString(), out cornerRadius);

        }

        if (context.Request.QueryString["cornercolor"] != null)
        {
            cornerColor = context.Request.QueryString["cornercolor"].ToString();

        }


        int requestedWidth = 0;
        int requestedHeight = 0;

        string imageFileName = context.Request.MapPath(ApplicationPath + "/"  + imgUrl);
        if (context.Request.Headers["If-Modified-Since"] != null)
        {
            FileInfo fileInfo = new FileInfo(imageFileName);
            double timeDiffInMillisecond = fileInfo.LastWriteTime.Subtract(DateTime.Parse(context.Request.Headers["If-Modified-Since"].ToString())).TotalMilliseconds;
            if (timeDiffInMillisecond < 6000)
            {
                context.Response.Status = "304 Not Modified";
                context.Response.Flush();
                context.Response.End();

                return;
            }
        }
        //context.Response.Cache.SetETagFromFileDependencies();


        Image inputImage = Image.FromFile(imageFileName, true);

        if (!String.IsNullOrEmpty(width))
        {
            requestedWidth = int.Parse(width);
            requestedHeight = (int)(((float)inputImage.Height / inputImage.Width) * requestedWidth);
        }
        else if (!String.IsNullOrEmpty(dimension))
        {
            requestedWidth = int.Parse(dimension.Split('x')[0]);
            requestedHeight = int.Parse(dimension.Split('x')[1]);
        }
        else
        {
            requestedWidth = inputImage.Width;
            requestedHeight = inputImage.Height;
        }

        string content_type = "image/jpeg";
        if (inputImage.RawFormat == System.Drawing.Imaging.ImageFormat.Gif)
        {
            content_type = "image/gif";
        }
        else if (inputImage.RawFormat == System.Drawing.Imaging.ImageFormat.Png)
        {
            content_type = "image/png";
        }

        if ((inputImage.Width == requestedWidth && inputImage.Height == requestedHeight) && String.IsNullOrEmpty(cropto))
        {
            //no need to resize the image

            if (cornerRadius != null && cornerRadius > 0)
            {

                inputImage = this.RoundCorners(inputImage, cornerRadius, Color.FromName(cornerColor));
            }

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.ContentType = content_type;


            context.Response.Cache.SetSlidingExpiration(true);

            context.Response.Cache.SetValidUntilExpires(true);

            context.Response.Cache.VaryByParams["*"] = true;

            //outputImage.Save(context.Response.OutputStream, GetEncoderInfo(content_type), encoderParams);        
            inputImage.Save(context.Response.OutputStream, GetImageFormat(content_type));

            inputImage.Dispose();
            context.Response.AddFileDependency(imageFileName);
            context.Response.Cache.SetETagFromFileDependencies();
            context.Response.Cache.SetExpires(DateTime.Now.AddTicks(600));
            context.Response.Cache.SetLastModifiedFromFileDependencies();
            context.Response.Cache.SetMaxAge(TimeSpan.FromDays(10));
            context.Response.Flush();
            context.Response.End();
            return;
        }

        //Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
        System.Drawing.Imaging.Encoder imgEncoder = System.Drawing.Imaging.Encoder.Quality;
        System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
        encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(imgEncoder, 100L);



        //Image outputImage = inputImage.GetThumbnailImage(requestedWidth, requestedHeight, myCallback, IntPtr.Zero);
        ////context.Response.Cache.SetCacheability(HttpCacheability.Public);
        //context.Response.ContentType = content_type;
        //outputImage.Save(context.Response.OutputStream, GetEncoderInfo(content_type), encoderParams);

        //outputImage.Dispose();

        //outputImage.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        if (!String.IsNullOrEmpty(cropto))
        {
            int left = 0;
            int top = 0;

            Bitmap _bPhoto = new Bitmap(requestedWidth, requestedHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            _bPhoto.SetResolution(inputImage.HorizontalResolution, inputImage.VerticalResolution);

            Graphics gfx = Graphics.FromImage(_bPhoto);

            if (cropto.Equals("center"))
            {
                left = (inputImage.Width / 2) - (requestedWidth / 2);
                top = (inputImage.Height / 2) - (requestedHeight / 2);
            }
            else
            {
                if (!int.TryParse(cropto.Split(',')[0], out left))
                {
                    throw new HttpException(404, "See InnerException for more info.", new ArgumentOutOfRangeException("width"));
                }
                if (!int.TryParse(cropto.Split(',')[1], out top))
                {
                    throw new HttpException(404, "See InnerException for more info.", new ArgumentOutOfRangeException("height"));
                }
            }

            gfx.DrawImage(inputImage, new Rectangle(0, 0, requestedWidth, requestedHeight), left, top, requestedWidth, requestedHeight, GraphicsUnit.Pixel);

            Image img = _bPhoto;
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            // context.Response.AddFileDependencies(

            //context.Response.Cache.SetETagFromFileDependencies()

            //      context.Response.Cache.SetLastModifiedFromFileDependencies()

            //     context.Response.Cache.SetCacheability(HttpCacheability.[Public])

            context.Response.ContentType = content_type;
            img.Save(context.Response.OutputStream, GetEncoderInfo(content_type), encoderParams);
            img.Dispose();
            _bPhoto.Dispose();
            gfx.Dispose();
        }
        else
        {

         //   inputImage.
            Image img = S_ImageManager.ResizeRatio(inputImage, requestedWidth, requestedHeight);//.Save(_ms, _codec, _params);
            if (cornerRadius != null && cornerRadius > 0)
            {

                img = this.RoundCorners(img, cornerRadius, Color.FromName(cornerColor));
            }
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.ContentType = content_type;
            img.Save(context.Response.OutputStream, GetEncoderInfo(content_type), encoderParams);
            img.Dispose();
        }
        inputImage.Dispose();

        //_aMs = _ms.ToArray();

        //_response.OutputStream.Write(_aMs, 0, _aMs.Length);
        //imageFileName
        context.Response.AddFileDependency(imageFileName);
        context.Response.Cache.SetETagFromFileDependencies();
        context.Response.Cache.SetExpires(DateTime.Now.AddTicks(600));
        context.Response.Cache.SetLastModifiedFromFileDependencies();
        context.Response.Cache.SetMaxAge(TimeSpan.FromDays(10));

        context.Response.Cache.SetSlidingExpiration(true);

        context.Response.Cache.SetValidUntilExpires(true);

        context.Response.Cache.VaryByParams["*"] = true;
        context.Response.Flush();
        context.Response.End();


    }
    //public System.Drawing.Imaging.ImageFormat GetImageFormat(string contentType)
    //{


    //}

    public Image RoundCorners(Image StartImage, int CornerRadius, Color BackgroundColor)
    {
        CornerRadius *= 2;
        Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
        Graphics g = Graphics.FromImage(RoundedImage);
        g.Clear(BackgroundColor);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Brush brush = new TextureBrush(StartImage);
        GraphicsPath gp = new GraphicsPath();
        gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
        gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
        gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
        gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);

        g.FillPath(brush, gp);

        return RoundedImage;
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public bool ThumbnailCallback()
    {
        return false;
    }


    private System.Drawing.Imaging.ImageFormat GetImageFormat(String mimeType)
    {

        if (mimeType == "image/gif")
        {
            return System.Drawing.Imaging.ImageFormat.Gif;
        }
        else if (mimeType == "image/png")
        {
            return System.Drawing.Imaging.ImageFormat.Png;
        }
        else
        {
            return System.Drawing.Imaging.ImageFormat.Jpeg;

        }


    }

    private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;

        System.Drawing.Imaging.ImageCodecInfo[] encoders;
        encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {

            if (encoders[j].MimeType == mimeType)
                return encoders[j];

        }
        return null;

    }


}