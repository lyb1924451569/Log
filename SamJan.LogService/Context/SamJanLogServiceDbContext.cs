using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamJan.LogService.Domain;
using SamJan.LogService.Logger;
using System.Reflection;

namespace SamJan.LogService.Context
{
    public class SamJanLogServiceDbContext : DbContext
    {
        //自定义EF 输出控制台日志
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new EFLoggerProvider());
        });

        private readonly IConfiguration _configuration;

        /// <summary>
        /// 接收的HL7消息日志表
        /// </summary>
        public DbSet<ReceiveLog> ReceiveLogs { get; set; }

        /// <summary>
        /// 发送的HL7消息日志表
        /// </summary>
        public DbSet<SendLog> SendLogs { get; set; }

        public SamJanLogServiceDbContext(DbContextOptions<SamJanLogServiceDbContext> options,
            IConfiguration configuration) 
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //添加日志输出
            optionsBuilder.UseLoggerFactory(loggerFactory);
            //显示隐私数据
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnectionStrings"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ReceiveLog>(b=>
            {
                b.ToTable("HL7Log");
            });

            modelBuilder.Entity<SendLog>(b=>
            {
                b.ToTable("HL7SendMessageLog");
            });
        }
    }
}
