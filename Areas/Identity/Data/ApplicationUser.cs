using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Areas.Identity.Data;

/// <summary>
/// Профиль для пользователей приложения
/// </summary>
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [DisplayName("Имя")]
    public string FirstName {  get; set; }
    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [DisplayName("Отчество")]
    public string LastName { get; set; }

    
}

