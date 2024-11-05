using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
	public interface INhanVienService
	{
		Task<IEnumerable<NhanVien>> GetAllNhanVienAsync();

		Task<NhanVien?> GetNhanVienByIdAsync(Guid id);

		Task AddNhanVienAsync(NhanVienDtos nvRequest);

		Task UpdateNhanVienAsync(NhanVienDtos nhanVien);

		Task DeleteNhanVienAsync(Guid id);
	}
}
