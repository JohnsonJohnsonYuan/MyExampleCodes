# Contents

## Notes

make alias of subquery because query needs table object which we will get from making an alias to subquery.

```sql
SELECT name FROM (SELECT name FROM agentinformation)        -- wrong
SELECT name FROM (SELECT name FROM agentinformation) as a   -- correct
```

## Pagination

### mysql pagination

[Format](http://www.mysqltutorial.org/mysql-limit.aspx)

```sql
SELECT
    column1,column2,...
FROM
    table
LIMIT `offset` , `count`;
```

The offset specifies the offset of the first row to return. The offset of the first row is `0`, not 1.
The count specifies the maximum number of rows to return

When use LIMIT with one argument, this means maximum number of rows to return from the data set.
`SELECT column1,column2,... FROM table LIMIT count`
is equivalent to
`SELECT column1,column2,... FROM table LIMIT 0, count`


## Index

### create index

```sql
CREATE INDEX [IndexName] ON [TableName]([Field1], [Field2], ...)

-- e.g.
CREATE INDEX [IxProduct3] ON [Product]([Category], [Name])
```

### get all indexes in table

```sql
exec sp_helpindex [TableName]
```

### delete index

```sql
if exists(select name from sysindexes  where name = '[index_name]')
DROP INDEX table_name.index_name

-- e.g.
if exists(select name from sysindexes
         where name = 'kk')
drop index student.kk
```

### sql server system table

* [sys.tables](https://docs.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-tables-transact-sql) Returns a row for each user table in SQL Server. `Prefer use this to select user table, avoid using type from sys.tables`
    ```sql
    SELECT * FROM sys.tables where name='Employee'
    ```
* [sys.objects](https://docs.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-objects-transact-sql)
    e.g. Get table called 'cars'
    ```sql
    select * from sys.objects where Name='cars' and type='U'
    ```
* [sys.sysobjects](https://docs.microsoft.com/en-us/sql/relational-databases/system-compatibility-views/sys-sysobjects-transact-sql) `Prefer use sys.objects(sysobjects是sql 2000 table, objects是view, 结果都一样)` This SQL Server 2000 system table is included as a view for backward compatibility. Find corresponding view from [Mapping System Tables to System Views](https://docs.microsoft.com/en-us/sql/relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql)