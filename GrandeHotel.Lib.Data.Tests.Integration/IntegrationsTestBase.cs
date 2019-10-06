﻿using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Impl;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace GrandeHotel.Lib.Data.Tests.Integration
{
    public class IntegrationsTestBase
    {
        // private const string ContentRootLocation = "..\\..\\..\\..\\TigerPistol.Video";
        public const string TestConnectionString = "Server=(local);Database=GrandeHotel_test;Trusted_Connection=True;";
        public const string RoundhouseStartLocation = "..\\..\\..\\..\\GrandeHotel.Lib.Data.Migrations\\bin\\debug\\netstandard2.0";
        public const string TestDataLocation = "TestSqlScripts";

        protected readonly UnitOfWork _unitOfWork;

        public IntegrationsTestBase()
        {
            DbContextOptionsBuilder<GrandeHotelContext> optionsBuilder = new DbContextOptionsBuilder<GrandeHotelContext>();
            var options = optionsBuilder.UseSqlServer(TestConnectionString);
            _unitOfWork = new UnitOfWork(new GrandeHotelCustomContext(options.Options));
        }


        public void ClearDatabase()
        {
            ProcessStartInfo processInfo = GetRHProcess();
            processInfo.Arguments = $"--cs \"{TestConnectionString}\" --drop --silent";
            Process process = Process.Start(processInfo);
            process.WaitForExit();
            if (process.ExitCode != 0) throw new Exception();
        }

        public void MigrateDatabase()
        {
            ProcessStartInfo processInfo = GetRHProcess();
            processInfo.Arguments = $"--cs \"{TestConnectionString}\" -f ChangeScripts --silent";
            var process = Process.Start(processInfo);
            process.WaitForExit();
            if (process.ExitCode != 0) throw new Exception();
        }

        private ProcessStartInfo GetRHProcess()
        {
            string rhDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), RoundhouseStartLocation);
            ProcessStartInfo output = new ProcessStartInfo("rh.exe");
            output.WorkingDirectory = rhDirectory;
            output.WindowStyle = ProcessWindowStyle.Normal;
            output.UseShellExecute = true;
            output.CreateNoWindow = false;
            output.LoadUserProfile = true;
            return output;

        }

        public void ExecuteScript(string sqlFile)
        {
            string query = File.ReadAllText(Path.Combine(TestDataLocation, sqlFile));
            using(SqlConnection conn = new SqlConnection(TestConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _unitOfWork?.Dispose();
        }
    }
}
