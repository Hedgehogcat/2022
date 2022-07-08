using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using HangfireDemo.Config;
using HangfireDemo.Configuration;
using HangfireDemo.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//添加本地路径获取支持
builder.Services.AddSingleton(new AppSettingsHelper(builder.Environment.ContentRootPath));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//注册Hangfire定时任务
builder.Services.AddHangFireSetup();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//授权
var filter = new BasicAuthAuthorizationFilter(
              new BasicAuthAuthorizationFilterOptions
              {
                  SslRedirect = false,
                  // Require secure connection for dashboard
                  RequireSsl = false,
                  // Case sensitive login checking
                  LoginCaseSensitive = false,
                  // Users
                  Users = new[]
                   {
                        new BasicAuthAuthorizationUser
                        {
                            Login = AppSettingsConstVars.HangFireLogin,
                            PasswordClear = AppSettingsConstVars.HangFirePassWord
                        }
                   }
              });
var options = new DashboardOptions
{
    AppPath = "/",//返回时跳转的地址
    DisplayStorageConnectionString = true,//是否显示数据库连接信息
    Authorization = new[]
               {
                    filter
                },
    IsReadOnlyFunc = Context =>
    {
        return false;//是否只读面板
    }
};

app.UseHangfireDashboard("/job", options); //可以改变Dashboard的url
//自动取消订单任务
//RecurringJob.AddOrUpdate<AutoCancelOrderJob>(s => s.Execute(), "0 0/2 * * * ? ", TimeZoneInfo.Local); // 每2分钟取消一次订单
//自动取消订单任务
//RecurringJob.AddOrUpdate<AutoCanclePinTuanJob>(s => s.Execute(), "0 0/2 * * * ? ", TimeZoneInfo.Local); // 每2分钟取消一次订单
BackgroundJob.Schedule<AutoCanclePinTuanJob>(s => s.Execute(),TimeSpan.FromMinutes(1));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
