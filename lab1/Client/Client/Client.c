#include <winsock2.h>
#include <Windows.h>
#include <stdio.h>
#include <string.h>
#define NewPort 5000
#define NewIp "127.0.0.1"


char buff[1024];
struct sockaddr_in NewAddr;


DWORD WINAPI Sending(LPVOID SocketList)
{
	SOCKET sock = ((SOCKET*)SocketList)[0];
	char buff[1024];
	int RecvMess = 0;
	while ((RecvMess = recv(sock, &buff, sizeof(buff), 0)))
	{
		buff[RecvMess] = '\0';
		printf("%s:  ", buff);
		RecvMess = recv(sock, &buff, sizeof(buff), 0);
		buff[RecvMess] = '\0';
		printf("%s\n", buff);
	}

}


void main()
{
	int port = 0;
	char ip[30];
	WSAStartup(0x0202, (WSADATA *)&buff[0]);
	SOCKET NewSocket = socket(AF_INET, SOCK_STREAM, 0);

	input:
	printf("Input port and ip\n");
	scanf("%d %s", &port, ip);

	NewAddr.sin_family = AF_INET;
	NewAddr.sin_port = port;
	NewAddr.sin_addr.s_addr = inet_addr(ip);
	if (connect(NewSocket, (struct sockaddr *)&NewAddr, sizeof(NewAddr)))
	{
		printf("Connection Errror\n");
		goto input;
	}

	//Read name
	char name[15];
	rename:
	printf("Input your name\n");
	scanf("%s", name);
	rewind(stdin);
	send(NewSocket, name, strlen(name), 0);
	int RecvMess = recv(NewSocket, &buff, sizeof(buff), 0);
	buff[RecvMess] = '\0';
	if (!(strcmp(buff, "f")))
		goto rename;

	printf("\n///////// WELCOME \\\\\\\\\\\n\n");
	DWORD thID;
	HANDLE thread = CreateThread(NULL, NULL, Sending, &NewSocket, NULL, &thID);
	strcpy(buff, "init");

	while (strcmp(buff, "quit"))
	{
		gets(buff);
		if (!strcmp(&buff[0], "quit"))
		{
			send(NewSocket, buff, strlen(buff), 0);
			send(NewSocket, name, strlen(name), 0);
			printf("Bye\n");
			system("pause");
			CloseHandle(thread);
		}
		else
		{
			send(NewSocket, name, strlen(name), 0);
			send(NewSocket, buff, strlen(buff), 0);
		}
	}

	//closesocket(NewSocket);
	//WSACleanup();
	return 0;
}