# Stage 1: Build
FROM mcr.microsoft.com/mssql/server:2022-latest AS build
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Password123*

WORKDIR /tmp
COPY /backup/Simplify.bak /var/opt/mssql/backup/Simplify.bak
COPY /backup/RestoreSimplifyDB.sql .

# Start SQL Server, wait for it to be ready, and restore the database
RUN /opt/mssql/bin/sqlservr --accept-eula & \
    sleep 40 && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "$MSSQL_SA_PASSWORD" -d master -i /tmp/RestoreSimplifyDB.sql -C && \
    ls -l /var/opt/mssql/data && \
    pkill sqlservr

# Debugging: Check that backup and data files exist
RUN ls -l /var/opt/mssql/backup && \
    ls -l /var/opt/mssql/data

# Stage 2: Release
FROM mcr.microsoft.com/mssql/server:2022-latest AS release
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Password123*

# Copy the restored database files from the build stage
COPY --from=build /var/opt/mssql/data /var/opt/mssql/data
COPY --from=build /var/opt/mssql/backup /var/opt/mssql/backup

# Debugging: Verify files in the release stage
RUN ls -l /var/opt/mssql/data && \
    ls -l /var/opt/mssql/backup

# Run SQL Server
CMD ["/opt/mssql/bin/sqlservr"]
