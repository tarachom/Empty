# Чиста конфігурація для розробки
 <img src="https://accounting.org.ua/images/preferences.png?v=3" /> <b>Базові класи і функції для розробки нової програми або навчання </b> | .net 8, Linux, Windows <br/>

 <hr />

 <b>Встановлення dotnet-sdk для Ubuntu</b>
 
 [Install the .NET SDK or the .NET Runtime on Ubuntu](https://learn.microsoft.com/uk-ua/dotnet/core/install/linux-ubuntu#register-the-microsoft-package-repository)<br/>

    # Get Ubuntu version
    declare repo_version=$(if command -v lsb_release &> /dev/null; then lsb_release -r -s; else grep -oP '(?<=^VERSION_ID=).+' /etc/os-release | tr -d '"'; fi)

    # Download Microsoft signing key and repository
    wget https://packages.microsoft.com/config/ubuntu/$repo_version/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

    # Install Microsoft signing key and repository
    sudo dpkg -i packages-microsoft-prod.deb

    # Clean up
    rm packages-microsoft-prod.deb

    # Update packages
    sudo apt update

    # Встановлення sdk
    sudo apt-get install -y dotnet-sdk-8.0

    # Встановлення runtime
    sudo apt-get install -y aspnetcore-runtime-8.0
    
    # Переглянути детальну інформацію про встановлені версії sdk і runtimes
    dotnet --list-sdks && dotnet --list-runtimes

<br/>

 <b>Встановлення PostgreSQL для Ubuntu</b>
 
[PostgreSQL](https://www.postgresql.org/download/linux/ubuntu/)<br/>
 
    sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" > /etc/apt/sources.list.d/pgdg.list'
    wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -
    
    sudo apt-get update
    
    sudo apt-get -y install postgresql

    # Встановлення пароля для postgres
    sudo -u postgres psql
    \password postgres
    
    # Переглянути детальну інформацію про встановлену програму postgresql
    dpkg -l | grep postgresql

<br/>

 <b>Встановлення Git</b>
    
    sudo apt install git

<br/>

 <b>Клонування репозиторіїв</b>
    
    git clone https://github.com/tarachom/Empty.git
    git clone https://github.com/tarachom/Configurator3.git
    git clone https://github.com/tarachom/AccountingSoftwareLib.git
    
<hr />
 
  Детальніше [accounting.org.ua](https://accounting.org.ua)<br/>
  Середовище розробки [Visual Studio Code](https://code.visualstudio.com)<br/>
  База даних [PostgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)<br/>