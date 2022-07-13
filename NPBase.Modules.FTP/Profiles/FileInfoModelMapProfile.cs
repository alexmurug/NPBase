using AutoMapper;
using FluentFTP;
using NPBase.Modules.FTP.Models;

namespace NPBase.Modules.FTP.Profiles;

public class FileInfoModelMapProfile : Profile
{
    public FileInfoModelMapProfile()
    {
        CreateMap<FtpListItem, FileInfoModel>()
             .ForMember(d => d.FullPath, o => o.MapFrom(s => s.FullName))
             .ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.Created))
             .ForMember(d => d.ModifiedDate, o => o.MapFrom(s => s.Modified));
    }
}
