CREATE USER  hrinventory;

alter user hrinventory with encrypted password 'hrinventory@123';

CREATE DATABASE hrinventorydb;

GRANT ALL PRIVILEGES ON DATABASE hrinventorydb TO hrinventory;