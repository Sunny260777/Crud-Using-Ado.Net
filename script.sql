USE [master]
GO
/****** Object:  Database [Layers]    Script Date: 03-05-2022 21:49:55 ******/
CREATE DATABASE [Layers]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Layers', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Layers.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Layers_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Layers_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Layers] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Layers].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Layers] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Layers] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Layers] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Layers] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Layers] SET ARITHABORT OFF 
GO
ALTER DATABASE [Layers] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Layers] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Layers] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Layers] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Layers] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Layers] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Layers] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Layers] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Layers] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Layers] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Layers] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Layers] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Layers] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Layers] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Layers] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Layers] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Layers] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Layers] SET RECOVERY FULL 
GO
ALTER DATABASE [Layers] SET  MULTI_USER 
GO
ALTER DATABASE [Layers] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Layers] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Layers] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Layers] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Layers] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Layers] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Layers', N'ON'
GO
ALTER DATABASE [Layers] SET QUERY_STORE = OFF
GO
USE [Layers]
GO
/****** Object:  Table [dbo].[usermaster]    Script Date: 03-05-2022 21:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usermaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[Mobile] [varchar](16) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[USP_DEL]    Script Date: 03-05-2022 21:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_DEL] (
@id varchar(15),
@table varchar(20),
@primary_key varchar(20),
@result varchar(200) output
)
as begin
set nocount on;
declare @sql varchar(200)

set @sql='DELETE FROM ' + @table + '  WHERE  ' + @primary_key + ' = ' + @id + '';
exec(@sql);
if(@@ROWCOUNT>0)
set  @result='Record Deleted Successfully'
else
set  @result='Error'


end
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_UPD_USERMASTER]    Script Date: 03-05-2022 21:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_INS_UPD_USERMASTER]
(
@id int=0,
@name varchar(20),
@mobile varchar(16),
@choice varchar(10),
@result varchar(200) output
)
as begin
set nocount on;
	
if(@choice='Insert')
	begin
		if not exists(select 1 from usermaster where name=@name )
		begin
			INSERT INTO usermaster (Name,Mobile) VALUES(@name,@mobile);
			if(@@ROWCOUNT>0)
			set  @result='Success'
			else
			set  @result='Error'
		end
		else begin
			set  @result='Record Already Exists'
		end			
	end
else if(@choice='Update')
	begin
		if not exists(select 1 from usermaster where name=@name and id!=@id )
		begin
			UPDATE usermaster SET name=@name,Mobile=@mobile WHERE id=@id;
			if(@@ROWCOUNT>0)
			set  @result='Success'
			else
			set  @result='Error'
		end
	    else begin
			set  @result='Record Already Exists'
		end

		
	end

end
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_BY_ID]    Script Date: 03-05-2022 21:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_SEL_BY_ID] (
@id varchar(15),
@column varchar(5000),
@table varchar(550),
@primary_key varchar(20),
@result varchar(200) output
)
as begin
set nocount on;
declare @sql varchar(200)

set @sql='SELECT ' + @column + ' FROM ' + @table + '  WHERE  ' + @primary_key + ' =  '+ @id + '';
exec(@sql);
if(@@ROWCOUNT>0)
set  @result='Success'
else
set  @result='Error'


end
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_Listing]    Script Date: 03-05-2022 21:49:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_SEL_Listing] (
@column varchar(5000)='',
@table varchar(550)='',
@order_by varchar(150)='',
@where varchar(500)='',
@start_index int=0,
@paging_size int=0,
@result varchar(200) output
)
as begin
set nocount on;
declare @sql varchar(max);
declare @recordcount varchar(max);

set @sql =CONCAT('SELECT ',
			@column,
			' FROM ',
			@table,
			case when @where='' then '' ELSE CONCAT(' WHERE ',@where) end ,
			case when @order_by='' then ' order by 1' ELSE CONCAT(' order by ',@order_by) end ,
			case when @start_index=0 then ' OFFSET 0 ROWS ' ELSE CONCAT(' OFFSET ',@start_index,' ROWS') END,
			case when @paging_size=0 then ' FETCH NEXT 10 ROWS ONLY ' ELSE CONCAT(' FETCH NEXT ',@paging_size,' ROWS ONLY ') END,' ;');


set @recordcount=CONCAT('SELECT Count(1) recordcount,',
			@paging_size,' pagesize,',
			(case when @start_index=0 then '1' ELSE @start_index END),' pageindex',
			' FROM ',
			@table,
			case when @where='' then '' ELSE CONCAT(' WHERE ',@where) END ,' ; ');	


			print(@sql);
			exec(@sql);

			print(@recordcount);
			exec(@recordcount);
		
if(@@ROWCOUNT>0)
set  @result='Success'
else
set  @result='Error'

end
GO
USE [master]
GO
ALTER DATABASE [Layers] SET  READ_WRITE 
GO
