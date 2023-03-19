using System;
using System.Collections.Generic;

namespace POC_WebAPI_DataAccessLayer.Models;

public partial class FileDetails
{
    public int Id { get; set; }

    public int Sid { get; set; }

    public string FileName { get; set; } = null!;

    public byte[] FileData { get; set; } = null!;

    public int FileType { get; set; }

    public virtual Student SidNavigation { get; set; } = null!;
}
