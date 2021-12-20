//ef command
dotnet tool install --global dotnet-ef

dotnet tool update --global dotnet-ef

dotnet ef

dotnet ef migrations add init

dotnet ef database update

dotnet ef migrations remove

dotnet ef migrations list

dotnet ef migrations script

//=========================================docker=========================================
1、下载docker镜像docker pull mariadb
2、查看本地已有的所有镜像docker images
3、建一个目录作为和容器的映射目录sudo mkdir -p /data/mariadb
4、启动镜像
docker run -d -p 8806:3306 --name db_max -v /Users/taojy/data/db_max:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456 mariadb --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci
--name启动容器设置容器名称为mariadb
-d后台运行容器mariadb并返回容器id
-p设置容器的3306端口映射到主机3306端口
-v设置容器目录/var/lib/mysql映射到本地目录/data/mariadb/data
-e MYSQL_ROOT_PASSWORD设置环境变量数据库root用户密码
--character-set-server=utf8mb4设置 utf-8字符集
--collation-server=utf8mb4_unicode_ci修改指定数据库的Collation

$ docker run -itd --name mysql-test -p 3306:3306 -e MYSQL_ROOT_PASSWORD=123456 mysql
参数说明：
-p 3306:3306 ：映射容器服务的 3306 端口到宿主机的 3306 端口，外部主机可以直接通过 宿主机ip:3306 访问到 MySQL 的服务。
MYSQL_ROOT_PASSWORD=123456：设置 MySQL 服务 root 用户的密码。

docker run -itd --name m1 -p 3306:3306 -e MYSQL_ROOT_PASSWORD=123456 mariadb

1）进入容器内部
sudo docker exec -it 容器名或ID /bin/bash
2）安装vim命令
更新软件列表
apt-get update
安装vim命令
apt-get install vim

sudo docker exec -it db_max /bin/bash

docker run -itd --name db_max -p 8806:3306 -e MYSQL_ROOT_PASSWORD=123456 mariadb



/mysql 
GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' IDENTIFIED BY '123456' WITH GRANT OPTION;
限定ip
GRANT ALL PRIVILEGES ON *.* TO 'root'@'10.110.30.60' IDENTIFIED BY '123456' WITH GRANT OPTION;
flush privileges;
