#include <winsock2.h>
#include <Windows.h>
#include <stdio.h>
#include <string.h>
#define NewPort 5000
#define NewIp 0
#define CountOfUsers 10


SOCKET SocketList[CountOfUsers + 1];
char NameList[10][30];


DWORD WINAPI Processing(LPVOID SocketList)
{
	SOCKET sock = ((SOCKET*)SocketList)[0];
	char buff[1024];
	int RecvMess = 0;
	while ((RecvMess = recv(sock, &buff, 1024, 0)) && (RecvMess != SOCKET_ERROR))
	{
		buff[RecvMess] = '\0';
		if(strcmp(buff, "quit"))
			for (char i = 1; i < CountOfUsers + 1; i++)
			{
				if (((SOCKET*)SocketList)[i] && sock != ((SOCKET*)SocketList)[i])
					send(((SOCKET*)SocketList)[i], &buff[0], RecvMess, 0);

			}
		else
		{
			RecvMess = recv(sock, &buff, 1024, 0);
			buff[RecvMess] = '\0';
			printf("%s disconnected\n", buff);
			for (char i = 0; i < CountOfUsers; i++)
			{
				if (!(strcmp(buff, NameList[i])))
					strcpy(NameList[i], "");
				if (((SOCKET*)SocketList)[i] && sock == ((SOCKET*)SocketList)[i])
				{
					closesocket(((SOCKET*)SocketList)[i]);
					((SOCKET*)SocketList)[i] = 0;
				}
			}
			closesocket(sock);
		}
	}

	closesocket(sock);
	return 0;
}


void main()
{
	char buff[1024];
	int port = 0;
	WSAStartup(0x0202, (WSADATA *)&buff[0]);
	SOCKET NewSocket = socket(AF_INET, SOCK_STREAM, 0);
	
	input:
	printf("Input port\n");
	scanf("%d", &port);

	struct sockaddr_in NewAddr;
	int iResult = 0, iError = 0;
	NewAddr.sin_family = AF_INET;
	NewAddr.sin_port = port;
	NewAddr.sin_addr.s_addr = NewIp;
	iResult = bind(NewSocket, (struct sockaddr*)&NewAddr, sizeof(NewAddr));
	if (iResult == SOCKET_ERROR) 
	{
		iError = WSAGetLastError();
		if (iError == WSAEACCES)
			printf("bind failed with WSAEACCES (access denied)\n");
		else
			printf("bind failed with error: %ld\n", iError);
		goto input;
	}

	listen(NewSocket, 100);

	SOCKET ClientSocket;
	struct sockaddr_in ClientAddr;
	int size = sizeof(ClientAddr);
	for (char i = 0; i < CountOfUsers; i++)
		SocketList[i] = 0;

	while ((ClientSocket = accept(NewSocket, (struct sockaddr*)&ClientAddr, &size)))
	{
		//verify
		int RecvMess;
		rename:
		RecvMess = recv(ClientSocket, &buff, 1024, 0);
		buff[RecvMess] = '\0';
		char k = 0;
		while (strcmp(NameList[k], "") && k >= 0 & k <= CountOfUsers)
			if (strcmp(NameList[k], buff))
				k++;
			else
				k = -1;
		if (k >= 0)
		{
			strcpy(NameList[k], buff);
			printf("New User %s\n", buff);
			buff[0] = 't';
			buff[1] = '\0';
			send(ClientSocket, &buff[0], 2, 0);
		}
		else
		{
			buff[0] = 'f';
			buff[1] = '\0';
			send(ClientSocket, &buff[0], 2, 0);
			goto rename;
		}

		char t = 1;
		while (SocketList[t] && t < CountOfUsers)
			t++;

		SocketList[t] = SocketList[0] = ClientSocket;
		DWORD thID;
		CreateThread(NULL, NULL, Processing, &SocketList, NULL, &thID);
		t = 1;
	}

	return 0;
}