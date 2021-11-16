using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iMaxSys.Max.Media.Mime
{
    /// <summary>
    /// 默认MIME映射器，可以根据文件扩展名获取标准内容类型。
    /// </summary>
    public static class ContentTypeFilter
    {
        //压缩包 图片 音频视频 文档
        public static ReadOnlyCollection<MimeMappingItem> Items = new List<MimeMappingItem>
        {
//图片
new MimeMappingItem{Extension = "gif", MimeType = "image/gif"},
new MimeMappingItem{Extension = "jpe", MimeType = "image/jpeg"},
new MimeMappingItem{Extension = "jpeg", MimeType = "image/jpeg"},
new MimeMappingItem{Extension = "jpg", MimeType = "image/jpeg"},
new MimeMappingItem{Extension = "png", MimeType = "image/png"},

//视频
new MimeMappingItem{Extension = "m3u", MimeType = "audio/x-mpegurl"},
new MimeMappingItem{Extension = "mid", MimeType = "audio/x-midi"},
new MimeMappingItem{Extension = "midi", MimeType = "audio/x-midi"},
new MimeMappingItem{Extension = "mif", MimeType = "application/x-mif"},
new MimeMappingItem{Extension = "mov", MimeType = "video/quicktime"},
new MimeMappingItem{Extension = "movie", MimeType = "video/x-sgi-movie"},

new MimeMappingItem{Extension = "mp1", MimeType = "audio/x-mpeg"},
new MimeMappingItem{Extension = "mp2", MimeType = "audio/x-mpeg"},
new MimeMappingItem{Extension = "mp3", MimeType = "audio/x-mpeg"},
new MimeMappingItem{Extension = "mp4", MimeType = "video/mp4"},
new MimeMappingItem{Extension = "mp4", MimeType = "video/mpeg4"},
new MimeMappingItem{Extension = "mpa", MimeType = "audio/x-mpeg"},
new MimeMappingItem{Extension = "mpe", MimeType = "video/mpeg"},
new MimeMappingItem{Extension = "mpeg", MimeType = "video/mpeg"},
new MimeMappingItem{Extension = "mpega", MimeType = "audio/x-mpeg"},
new MimeMappingItem{Extension = "mpg", MimeType = "video/mpeg"},
new MimeMappingItem{Extension = "mpv2", MimeType = "video/mpeg2"},
new MimeMappingItem{Extension = "wmv", MimeType = "video/x-ms-wmv"},

new MimeMappingItem{Extension = "avi", MimeType = "video/x-msvideo"},

//压缩包
new MimeMappingItem{Extension = "rar", MimeType = "application/octet-stream"},
new MimeMappingItem{Extension = "zip", MimeType = "application/zip"},
new MimeMappingItem{Extension = "7z", MimeType = "application/x-7z-compressed"},



//文档
new MimeMappingItem{Extension = ".doc", MimeType = "application/msword"},
new MimeMappingItem{Extension = ".dot", MimeType = "application/msword"},

new MimeMappingItem{Extension = ".docx", MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
new MimeMappingItem{Extension = ".dotx", MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
new MimeMappingItem{Extension = ".docm", MimeType = "application/vnd.ms-word.document.macroEnabled.12"},
new MimeMappingItem{Extension = ".dotm", MimeType = "application/vnd.ms-word.template.macroEnabled.12"},

new MimeMappingItem{Extension = ".xls", MimeType = "application/vnd.ms-excel"},
new MimeMappingItem{Extension = ".xlt", MimeType = "application/vnd.ms-excel"},
new MimeMappingItem{Extension = ".xla", MimeType = "application/vnd.ms-excel"},

new MimeMappingItem{Extension = ".xlsx", MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
new MimeMappingItem{Extension = ".xltx", MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
new MimeMappingItem{Extension = ".xlsm", MimeType = "application/vnd.ms-excel.sheet.macroEnabled.12"},
new MimeMappingItem{Extension = ".xltm", MimeType = "application/vnd.ms-excel.template.macroEnabled.12"},
new MimeMappingItem{Extension = ".xlam", MimeType = "application/vnd.ms-excel.addin.macroEnabled.12"},
new MimeMappingItem{Extension = ".xlsb", MimeType = "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},

new MimeMappingItem{Extension = ".ppt", MimeType = "application/vnd.ms-powerpoint"},
new MimeMappingItem{Extension = ".pot", MimeType = "application/vnd.ms-powerpoint"},
new MimeMappingItem{Extension = ".pps", MimeType = "application/vnd.ms-powerpoint"},
new MimeMappingItem{Extension = ".ppa", MimeType = "application/vnd.ms-powerpoint"},

new MimeMappingItem{Extension = ".pptx", MimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
new MimeMappingItem{Extension = ".potx", MimeType = "application/vnd.openxmlformats-officedocument.presentationml.template"},
new MimeMappingItem{Extension = ".ppsx", MimeType = "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
new MimeMappingItem{Extension = ".ppam", MimeType = "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
new MimeMappingItem{Extension = ".pptm", MimeType = "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
new MimeMappingItem{Extension = ".potm", MimeType = "application/vnd.ms-powerpoint.template.macroEnabled.12"},
new MimeMappingItem{Extension = ".ppsm", MimeType = "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},

//new MimeMappingItem{Extension = ".mdb", MimeType = "application/vnd.ms-access"},

        }.AsReadOnly();






    }
}