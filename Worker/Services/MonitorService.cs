
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace Worker.Services
{
    public class MonitorService : IHostedService
    {
        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await AddJobHangFire();
        }

        private async Task AddJobHangFire()
        {
            //Agendado
            BackgroundJob.Schedule(() => Print("Agendamento", null), TimeSpan.FromSeconds(5));

            //Em Fila
            var jobId = BackgroundJob.Enqueue("test", () => Print("Test in Queue", null));

            //Roda um processo a partir de um Id Pai
            BackgroundJob.ContinueJobWith(jobId, () => Print($"Rodou Após Terminar o job id {jobId}", null));

            //Recorrente
            RecurringJob.AddOrUpdate("RecurringJob", () => PrintRecurringJob("Recurring", null), MinuteInterval(5));
        }

        public void Print(string message, PerformContext? context) 
        {
            context.WriteLine(message);
        }

        public void PrintRecurringJob(string message, PerformContext? context)
        {
            context.WriteLine("Inicio do Processo");

            Thread.Sleep(TimeSpan.FromSeconds(5));

            context.WriteLine("Processo está quase acabando.");

            Thread.Sleep(TimeSpan.FromSeconds(5));

            context.WriteLine("Processo finalizado.");
            context.WriteLine(message);
        }

        public static string MinuteInterval(int interval)
        {
            return $"*/{interval} * * * *";
        }
    }
}
