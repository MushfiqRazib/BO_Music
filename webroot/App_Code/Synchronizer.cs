/* Code@provas. Date: 27-Mar-09
 * Background: There is a page named 'Upload.aspx' in this website. By using this page user can upload 
 * differnt types of resources (for example: music, pdf, image etc) by synchornizing the database for that resourc.
 * But User can upload only one file at a time for differnt types of resources. 
 * If user need to upload 1000 files with synchronization then it will be time consuming
 * job for the user by selecting 1000 files and uploading one by one.
 * 
 * Objectives: By using this module user should be able to synchronize all resources with database by one click!!!
 *  
*/

using System;
using Npgsql;
using System.Drawing;
using System.IO;
using System.Collections;


/// <summary>
/// Summary description for Synchronizer
/// </summary>
public class Synchronizer
{
    public Synchronizer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Sychoronize resources
    /// <summary>
    /// Code@provas. Date: 27-Mar-09
    /// 1. Synchornize all necessary files
    /// 2. Make sure that unnecessary files (if any) will not create any problem
    /// 3. Avoid this function for newsimage
    /// 4. Synchronize all with database by a single connection
    /// 5. Return status if it is success or not
    /// </summary>
    /// <param name="resourcePath">the path of the resources</param>
    /// <returns>true if success otherwise false</returns>
    public bool Synchronize(string resourcePath)
    {
        bool status = false;
        ArrayList queryList = new ArrayList();
        string conStr = System.Configuration.ConfigurationManager.AppSettings["connection-string"].ToString();
       
        DbHandler handler = new DbHandler();
        NpgsqlConnection conn = new NpgsqlConnection(conStr);
        conn.Open();
        try
        {
            foreach (string directory in Functions.GetDirectories(resourcePath))
            {
                string folder = directory.Substring(directory.LastIndexOf("\\") + 1);
                //if (folder.ToLower().Equals("newsimage"))       //restric for NewsImage
                //{
                //    continue;
                //}

                if (ArticleFolder(folder))
                {
                    // Update different columns in the article table as empty for differnt types of article
                    queryList.Add(new NpgsqlCommand(GetQuery(folder)));

                    foreach (string file in Functions.GetFiles(directory))
                    {
                        string fileName = Functions.GetFileName(file);
                        string articleCode = Functions.GetArticleCode(fileName);
                        if (fileName.Equals(string.Empty))          //restric for unnecessary files
                        {
                            continue;
                        }
                        if (folder.ToLower().Equals("images"))
                        {
                            //resize image
                            if (!Functions.IsImageFile(fileName))
                            {
                                continue;
                            }
                            else
                            {
                                ResizeImage(directory, fileName);
                            }
                        }
                        queryList.Add(new NpgsqlCommand(GetQuery(folder, fileName, articleCode)));
                        //string query = GetQuery(folder, fileName, articleCode);
                        //handler.ExecuteQuery(new NpgsqlCommand(query), conn);                    
                    }
                }
            }
            handler.ExecuteTransaction(queryList);
            status = true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            status = false;
        }
        finally
        {
            conn.Close();
        }
        return status;
    }

    #endregion

    #region Resize Image
    private void ResizeImage(string directory, string fileName)
    {
        int thumbWidth = 94;
        int thumbHeight = 132;
        int largeWidth = 180;
        int largeHeight = 252;
      
        Bitmap bmpThumb = null;
        Bitmap bmpLarge = null;

        bmpThumb = Functions.CreateThumbnail(directory + "\\" + fileName, thumbWidth, thumbHeight);
        bmpLarge = Functions.CreateThumbnail(directory + "\\" + fileName, largeWidth, largeHeight);

        string thumbFileName = directory + "\\" + "Thumb_" + fileName;
        string largeFileName = directory + "\\" + fileName;
        try
        {
            if (File.Exists(thumbFileName))
            {
                File.Delete(thumbFileName);
            }
            if (File.Exists(largeFileName))
            {
                File.Delete(largeFileName);
            }
            bmpThumb.Save(thumbFileName);
            bmpLarge.Save(largeFileName);

        }
        catch (Exception ex)
        {
        }
        finally
        {
            bmpThumb.Dispose();
            bmpLarge.Dispose();
        }
    }
    #endregion

    #region Get Update query
    /// <summary>
    /// This method should returns a update query using the supplied parameter
    /// </summary>
    /// <param name="folder">Name of the folder</param>
    /// <param name="fileName">Name of the file</param>
    /// <param name="code">article code</param>
    /// <returns>Update query</returns>
    private string GetQuery(string folder, string fileName, string code)
    {
        string query = @"Update article set ";
        switch (folder.ToLower())
        {
            case "audio":
                query += @" containsmusic = true";
                break;
            case "images":
                query += @" imagefile='" + fileName + "'";
                break;
            case "pdf":
                query += @" pdffile='" + fileName + "'";
                break;
            default:
                break;
        }
        query += @" where articlecode='" + code + "'";
        return query;
    }

    /// <summary>
    /// Update different columns in the article table as empty or false
    /// depends on differnt types of article
    /// </summary>
    /// <param name="folder"></param>
    /// <returns></returns>
    private string GetQuery(string folder)
    {
        string query = @"Update article set ";
        switch (folder.ToLower())
        {
            case "audio":
                query += @" containsmusic = false";
                break;
            case "images":
                query += @" imagefile=''";
                break;
            case "pdf":
                query += @" pdffile=''";
                break;
            default:
                break;
        }
        return query;
    }

    #endregion
    private bool ArticleFolder(string folder)
    {
        switch (folder.ToLower())
        {
            case "audio":
            case "images":
            case "pdf":
                return true;
            default:
                return false;
        }
    }

}
