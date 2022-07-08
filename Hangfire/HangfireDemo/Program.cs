using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using HangfireDemo.Config;
using HangfireDemo.Configuration;
using HangfireDemo.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//��ӱ���·����ȡ֧��
builder.Services.AddSingleton(new AppSettingsHelper(builder.Environment.ContentRootPath));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ע��Hangfire��ʱ����
builder.Services.AddHangFireSetup();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//��Ȩ
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
    AppPath = "/",//����ʱ��ת�ĵ�ַ
    DisplayStorageConnectionString = true,//�Ƿ���ʾ���ݿ�������Ϣ
    Authorization = new[]
               {
                    filter
                },
    IsReadOnlyFunc = Context =>
    {
        return false;//�Ƿ�ֻ�����
    }
};

app.UseHangfireDashboard("/job", options); //���Ըı�Dashboard��url
//�Զ�ȡ����������
//RecurringJob.AddOrUpdate<AutoCancelOrderJob>(s => s.Execute(), "0 0/2 * * * ? ", TimeZoneInfo.Local); // ÿ2����ȡ��һ�ζ���
//�Զ�ȡ����������
//RecurringJob.AddOrUpdate<AutoCanclePinTuanJob>(s => s.Execute(), "0 0/2 * * * ? ", TimeZoneInfo.Local); // ÿ2����ȡ��һ�ζ���
BackgroundJob.Schedule<AutoCanclePinTuanJob>(s => s.Execute(),TimeSpan.FromMinutes(1));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
