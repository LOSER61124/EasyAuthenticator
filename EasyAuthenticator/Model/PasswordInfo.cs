using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAuthenticator.Model
{
    /// <summary>
    /// PasswordInfo 密码信息表
    /// </summary>
    public class PasswordInfo
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        [Column(IsIdentity =true)]
        public int Id { get; set; }

        /// <summary>
        /// 密码(不要明文)
        /// </summary>
        public string Pwd { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { get; set; }

        /// <summary>
        /// 软删除 0未删除 1已删除
        /// </summary>
        public int IsDelete { get; set; }
    }
}
