dotnet ef dbcontext scaffold "Server=.;Database=GrandeHotel;Trusted_Connection=true;" Microsoft.EntityFrameworkCore.SqlServer -o Models --schema reservations --context-dir . --force
pause