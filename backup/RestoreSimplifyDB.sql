RESTORE DATABASE [Simplify] FROM DISK = '/var/opt/mssql/backup/Simplify.bak'
WITH FILE = 1,
MOVE 'Simplify' TO '/var/opt/mssql/data/Simplify.mdf',
MOVE 'Simplify_log' TO '/var/opt/mssql/data/Simplify.ldf',
NOUNLOAD, REPLACE, STATS = 5;
GO