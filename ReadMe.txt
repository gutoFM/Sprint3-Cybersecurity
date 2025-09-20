### Read Me ###
PROJETO REALIZADO PELOS ALUNOS DA FIAP
Turma 3ESPX

Augusto Milreu – RM98245 
David Denunci - rm98603 
Fernando Popolili - rm99919 
Lucas de Toledo -rm97913 
Matheus Zanardi – rm 98832 



Sistemas Operacionais, instalações até a conclusão da aplicação

O que veremos:

1. VM Ubuntu Server criada e com internet
Ubuntu Server - ISO 22.04 LTS
Propriedades do Servidor  Virtualização VirtualBox
	2 vCPUs | 4 GB RAM | 25 GB disco dinâmico
	Network - Bridged Adapter

Verificação de conexão
> ip a


### Recomendação ###

Conexão com a VM através do Windows PowerShell

Após a conclusão da criação da ISO Ubuntu Server, ainda no ambiente virtualizado, vamos habilitar SSH no Ubuntu Server;

> sudo apt install -y openssh-server # Instalação do SSH server
> sudo systemctl status ssh # Verificação do status do SSH server
> sudo systemctl enable ssh # Habilitar para iniciar automaticamente após todo reboot
> hostname -I # Verfica o IP da VM para ser conectado através do SSH (utilizar Ipv4)


Voltando ao Windows, inicie o Windows PowerShell;

> ssh seu_usuario@ip.da.vm # Utilizar apenas o IPV4

Ocorrendo tudo corretamente, deve retornar uma mensagem do sistema perguntando se você quer se conectar com o fingerpoint;
Digite Yes para confirmação da conexão com a VM



2. Instalação Docker (docker --version) e NGINX (nginx -v).
Para iniciarmos o projeto vamos começar instalando o Docker e suas dependências no Ubuntu Server

# Atualizações e instalações de pacotes iniciais
> sudo apt update

# instalação de pacotes necessários para configurar repositórios de software no Ubuntu
> sudo apt install -y ca-certificates curl gnupg lsb-release 

# adicionar chave e repositório Docker ao OS
> curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] \
  https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" \
  | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# Atualizações e instalações de pacotes iniciais
> sudo apt update

# instalação do Docker e componentes relacionados no Ubuntu
> sudo apt install -y docker-ce docker-ce-cli containerd.io docker-compose-plugin

# Adicionar usuário ao grupo docker
> sudo usermod -aG docker $USER

# habilita e iniciar docker
> sudo systemctl enable Docker
> sudo systemctl start docker

# Verificar instalação
> docker --version


Agora vamos iniciar a instalação do NGINX

# inicia a instalação do NGINX
> sudo apt install -y nginx

# habilita e iniciar NGINX
> sudo systemctl enable nginx
> sudo systemctl start nginx

# Verificar status
systemctl status nginx

# Testar acesso
no navegador -> http://IP.DA.VM (o mesmo ip utilizado para fazer a conexão ssh com a VM)
ou
> curl http://IP.DA.VM

Com a instalação correta, é retornado uma mensagem web dizendo que o servidor web NGINX foi instalado corretamente e esta funcionando.



3. Container sqlite em execução e criação do arquivo ~/sqlite-data/app.db.
Vamos começar o processo de criação do arquivo .db localizado no nosso sistema do Ubuntu Server;

Começamos criando o diretório que ira se localizar nosso SQLite
> mkdir -p ~/sqlite-data

Verificação de criação do diretório
> ls -l

Entre no diretório criado para criação do Docker File para deixarmos o container em execução e seja possível manipular o arquivo via container;
> cd sqlite-data/
> cat > ~/sqlite-data/Dockerfile <<'EOF'

Agora com a exibição de dentro do Dockerfile, vamos configurar os parâmetros do shell linha por linha;
> FROM alpine:3.18
RUN apk add --no-cache sqlite
VOLUME /data
WORKDIR /data
CMD ["sh", "-c", "tail -f /dev/null"]
EOF

Ainda dentro do diretório SQLite-Data, verifique a criação do dockerfile e em seguida vamos começar a Buildar e rodar a imagem do Docker;
> ls -l
> docker build -t sqlite-image .
> docker run -d --name sqlite --restart unless-stopped -v ~/sqlite-data:/data sqlite-image

Após a instalação da imagem do SQLite dentro do container Docker, vamos montar um volume externo (da sua máquina Linux) onde seu arquivo .db vai ser localizado;
> docker exec -it sqlite sh -c "sqlite3 /data/app.db \"CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, cpf TEXT, rg TEXT, email TEXT, created_at DATETIME DEFAULT CURRENT_TIMESTAMP);\""



4. Endpoint Node /users funcionando.
Saia do diretório do SQLite-Data e em seguida execute os comandos de instalação do NODE 18;
> curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
> sudo apt install -y nodejs build-essential
> node -v
> npm -v

Após a instalação do Node, vamos criar o diretório da aplicação do web server;
> mkdir -p ~/app
> cd ~/app
> npm init -y

# Instala o Node.js e pacotes necessários para criação do web server (Express), Banco de dados (SQlite 3) e transformação de JSON para objetos JavaScript (Body-Parser)
> npm install express sqlite3 body-parser 

====================================================================================================
### SE DER ERRO DE TIMEOUT ###
Caso o erro de instalação do timeout persista, provavelmente a VM esta forçando conexão através do IPV6 e não conseguiu se conectar a internet. Para solucionar o problema comece avaliando os erros;

Tente pingar;
> ping -c 3 google.com 
Caso o ping tenha packet-loss, desative a utilização, temporária ou permanente, do IPV6;
> sudo sysctl -w net.ipv6.conf.all.disable_ipv6=1
> sudo sysctl -w net.ipv6.conf.default.disable_ipv6=1

Cria um /etc/resolv.conf novo e persistente só com DNS IPv4 confiáveis (Google + Cloudflare).
> sudo systemctl disable systemd-resolved --now && \
> sudo rm -f /etc/resolv.conf && \
> echo -e "nameserver 8.8.8.8\nnameserver 8.8.4.4\nnameserver 1.1.1.1" | sudo tee /etc/resolv.conf


Após as devidas configurações, o problema de TIMEOUT deve ser encerrado. Para verificação teste;
> ping -c 3 google.com # DEVE RETORNAR O PING COM OS 3 PACOTES RECEBIDOS
> curl -I https://registry.npmjs.org/ # O CURL DEVE RESPONDER COM HTTP/2 200
====================================================================================================


Para a criação do endpoint, vamos criar nossa aplicação em JS:
Crie o arquivo INDEX.JS dentro da pasta APP;
> touch index.js

Entre dentro do editor de texto do Linux (NANO)
> nano index.js

E então cole o código de criação do endpoint;
> // ~/app/index.js
const express = require('express');
const sqlite3 = require('sqlite3').verbose();
const bodyParser = require('body-parser');
const DB_PATH = '/home/SEU-USER/sqlite-data/app.db'; // ajuste se seu usuário/nome de pasta for diferente

const db = new sqlite3.Database(DB_PATH, (err) => {
  if(err) {
    console.error('Erro abrindo DB:', err.message);
    process.exit(1);
  }
  console.log('DB conectado em', DB_PATH);
});

const app = express();
app.use(bodyParser.json());

app.post('/users', (req, res) => {
  const { name, cpf, rg, email } = req.body;
  if (!name || !cpf) return res.status(400).json({ error: 'name and cpf required' });

  const stmt = db.prepare('INSERT INTO users (name, cpf, rg, email) VALUES (?,?,?,?)');
  stmt.run(name, cpf, rg || null, email || null, function(err){
    if (err) return res.status(500).json({ error: err.message });
    res.status(201).json({ id: this.lastID });
  });
  stmt.finalize();
});

app.get('/users', (req, res) => {
  db.all('SELECT id, name, cpf, rg, email, created_at FROM users', (err, rows) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(rows);
  });
});

app.listen(3000, () => console.log('API rodando na porta 3000'));


Para rodar digite no bash da pasta APP;
> node index.js

Se tudo ocorreu corretamente, deve retornar
> API rodando na porta 3000
DB conectado em /home/SEU-USER/sqlite-data/app.db


Teste a conexão através do CURL colocando o IPV4 da VM;
> curl -X POST http://SEU.IP.DA.VM:3000/users -H "Content-Type: application/json" -d '{"name":"Augusto Fisco","cpf":"123.456.789-09","rg":"12.345.678-9","email":"augustof@example.com"}'

Seu retorno deve ser;
> {"id":1}Seu-USER@Ubuntu-Server:~$


====================================================================================================
### SE DER ERRO DE PERMISSÂO DE ESCRITA NO ARQUIVO ###
Verifique o dono e permissões do seguinte arquivo;
> ls -l /home/SEU-USER/sqlite-data/app.db

# Caso retorne algo parecido com isso, o NODE não vai conseguir fazer alterações devido a falta de permissão;
> -r--r--r-- 1 root root 8192 set 18 00:10 /home/SEU-USER/sqlite-data/app.db

Dê permissão de escrita ao usuário atual;
> chmod 777 /home/SEU-USER/sqlite-data/app.db
> chmod 777 /home/SEU-USER/sqlite-data

Verifique novmente o dono e as permissões do arquivo;
> ls -l /home/SEU-USER/sqlite-data/app.db

# Com a conclusão do passo a passo, deve ser retornado da seguinte maneira;
> -rwxrwxrwx 1 root root 12288 Sep 18 19:00 /home/SEU-USER/sqlite-data/app.db

Dê CTRL C no servidor da VM para parar, e logo em seguida reinicie o app;
> node index.js

E tente novamente o CURL;
> curl -X POST http://SEU.IP.DA.VM:3000/users -H "Content-Type: application/json" -d '{"name":"Augusto Fisco","cpf":"123.456.789-09","rg":"12.345.678-9","email":"augustof@example.com"}'
====================================================================================================


5. NGINX reverso configurado (abra http://<VM_IP> e veja proxy).
Com uma camada adicional de proteção ao servidor, menos carga nos servidores da aplicação vamos implementar um reverse-proxy, garantindo uma conexão de acordo com o diagrama a seguir:

Cliente -> NGINX (porta 80) -> Node.js (porta 3000)


Para criação do reverse-proxy, vamos criar o bloco de site para que seja repassado ao NODE como medida de segurança;
> sudo tee /etc/nginx/sites-available/myapp <<'EOF'
> server {
  listen 80;
  server_name localhost;

  location / {
    proxy_pass http://SEU.IP.DA.VM:3000;
    proxy_http_version 1.1;
    proxy_set_header Upgrade $http_upgrade;
    proxy_set_header Connection 'upgrade';
    proxy_set_header Host $host;
    proxy_cache_bypass $http_upgrade;
  }
}
EOF


====================================================================================================
### CASO DÊ O ERRO ###
Erro esperado:
> sudo: unable to resolve host Ubuntu-Server: Name or service not known
# O sudo tentou resolver o hostname Ubuntu-Server, mas não encontrou no /etc/hosts

Para correção, descubra o nome da sua maquina através do seguinte comando;
> hostname

Entre no editor de texto (NANO) apontando para o seguinte diretório. ;
> sudo nano /etc/hosts

Dentro do nano;
> 127.0.0.1   localhost Ubuntu-Server
CTRL + S para salvar e em seguida CTRL + X para sair

Teste através do ping;
> ping -c 1 Ubuntu-Server


Com a conclusão do passo a passo, é esperado que o erro tenha sumido. Então tente novamente o > sudo see....
====================================================================================================


Após a solução do erro esperado, habilite os servidores e logo em seguida reboote ele;
> sudo ln -sf /etc/nginx/sites-available/myapp /etc/nginx/sites-enabled/
> sudo nginx -t && sudo systemctl reload nginx


E com isso concluimos nosso reverse-proxy, para conferência, entre no seu navegador e coloque;
> http://SEU.IP.DA.VM/users

Retorno esperado;
> 
[
  {
    "id": 1,
    "name": "Augusto Fisco",
    "cpf": "123.456.789-09",
    "rg": "12.345.678-9",
    "email": "augustof@example.com",
    "created_at": "2025-09-18 23:05:50"
  }
]



6. Script anonymize_pii.sh que cria backup e mascara PII; cron agendado.
Para iniciarmos, criamos um arquivo no diretório de nossa preferência
> sudo tee /usr/local/bin/anonymize_pii.sh > /dev/null <<'EOF'

Definimos aqui as propriedades que devem estar dentro do nosso shell script
> #!/bin/bash
DB="/home/gutofm/sqlite-data/app.db"
BACKUP_DIR="/home/SEU-USER/sqlite-data/backups"
mkdir -p "$BACKUP_DIR"
BACKUP="$BACKUP_DIR/backup_$(date +%F_%H%M%S).db"

# 1) Backup
cp "$DB" "$BACKUP" || { echo "Falha no backup"; exit 1; }

# 2) Anonimização simples (exemplos)
sqlite3 "$DB" <<SQL
-- manter a primeira letra do nome e apagar o resto
UPDATE users SET name = CASE WHEN name IS NOT NULL THEN substr(name,1,1) || ' *****' ELSE NULL END WHERE name IS NOT NULL;

-- mascarar CPF e RG (substitui por padrão fixo)
UPDATE users SET cpf = '***.***.***-**' WHERE cpf IS NOT NULL;
UPDATE users SET rg = '*******' WHERE rg IS NOT NULL;

-- se quiser remover e-mails:
UPDATE users SET email = NULL WHERE email IS NOT NULL;
SQL

echo "Anonymize done at $(date)"
EOF


Após a criação do arquivo e da definição das propriedades / configurações, escalamos o privilégio das permissões do arquivo;
> sudo chmod +x /usr/local/bin/anonymize_pii.sh

Para incrementar e aplicar a mascara de anonimização
> sudo diretorio-que-esta-o-sh/anonymize_pii.sh


## RECOMENDAÇÃO ##
Recomendo fortemente a cópia do script para a mesma pasta que está o diretório do servidor web, por motivos de facilitação de execução. Para concluir a recomendação, utilize o seguinte comando;
> sudo cp diretorio-que-esta-o-sh/anonymize_pii.sh /home/SEU-USER/app/
> sudo chmod +x /home/SEU-USER/app/anonymize_pii.sh


Obrigado por chegar até aqui! Qualquer dúvida que tiver me chame.