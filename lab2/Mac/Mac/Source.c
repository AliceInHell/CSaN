#ifndef UNICODE
#define UNICODE
#endif

#include <stdio.h>
#include <Windows.h>
#include <Iphlpapi.h>
#include <Assert.h>
#include <winnetwk.h>
#include <stdint.h>

#pragma comment(lib, "iphlpapi.lib")
#pragma comment(lib, "mpr.lib")
#pragma comment(lib, "Ws2_32.lib")

void getMAC();
void getNetworkMAC(char*, char*);
BOOL WINAPI EnumerateFunc(LPNETRESOURCE lpnr);
void DisplayStruct(int i, LPNETRESOURCE lpnrLocal);

char MyIp[16];
char trash[10];
char s[32];
char MyMask[16];


void getMAC() {
	PIP_ADAPTER_INFO AdapterInfo;
	DWORD dwBufLen = sizeof(AdapterInfo);
	char *mac_addr = (char*)malloc(17);

	AdapterInfo = (IP_ADAPTER_INFO *)malloc(sizeof(IP_ADAPTER_INFO));

	// Make an initial call to GetAdaptersInfo to get the necessary size into the dwBufLen     variable
	if (GetAdaptersInfo(AdapterInfo, &dwBufLen) == ERROR_BUFFER_OVERFLOW) {

		AdapterInfo = (IP_ADAPTER_INFO *)malloc(dwBufLen);
		if (AdapterInfo == NULL) {
			printf("Error allocating memory needed to call GetAdaptersinfo\n");
		}
	}

	if (GetAdaptersInfo(AdapterInfo, &dwBufLen) == NO_ERROR) {
		PIP_ADAPTER_INFO pAdapterInfo = AdapterInfo;// Contains pointer to current adapter info
		do {
			sprintf(mac_addr, "%02X:%02X:%02X:%02X:%02X:%02X",
				pAdapterInfo->Address[0], pAdapterInfo->Address[1],
				pAdapterInfo->Address[2], pAdapterInfo->Address[3],
				pAdapterInfo->Address[4], pAdapterInfo->Address[5]);
			printf("Address: %s, mac: %s, mask: %s\n", pAdapterInfo->IpAddressList.IpAddress.String, mac_addr, pAdapterInfo->IpAddressList.IpMask.String);
			

			printf("\n");
			if (strcmp(pAdapterInfo->IpAddressList.IpAddress.String, "0.0.0.0"))
			{
				strcpy(MyIp, pAdapterInfo->IpAddressList.IpAddress.String);
				strcpy(MyMask, pAdapterInfo->IpAddressList.IpMask.String);
			}
			pAdapterInfo = pAdapterInfo->Next;
		} while (pAdapterInfo);
	}
	free(AdapterInfo);
}


unsigned long swapBytes(unsigned long x)
{
	char c[33], r[33] = "";
	strcpy(s, "");
	itoa(x, s, 2);
	int i = 0;
	for (i = 0; i < 32 - strlen(s); i++)
		c[i] = '0';
	c[i] = '\0';
	strcat(c, s);
	c[32] = '\0';

	for (int j = 1; j < 5; j++)
	{
		for (int k = 0; k < 8; k++)
			r[k + (j - 1) * 8] = c[32 - j * 8 + k];
	}
	r[32] = '\0';
	s[0] = "\0";
	i = 0;

	while (r[i] == '0')
		i++;

		for (int j = i; j < 32; j++)
			s[j - i] = r[j];
	return strtoul(r, NULL, 2);
}


void getNetworkMAC()
{
	//get IP range
	unsigned long decIP = swapBytes(inet_addr(MyIp));
	unsigned long decMask = swapBytes(inet_addr(MyMask));
	unsigned long myNET = decIP & decMask;
	unsigned long range = ~decMask;
	struct in_addr ip_addr;
	char temp[32];

	//preparing for ping
	char buff[1024];
	WSAStartup(0x0202, (WSADATA *)&buff[0]);
	SOCKET NewSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	typedef struct {
		uint8_t type;
		uint8_t code;
		uint16_t chksum;
		uint32_t data;
	} icmp_hdr_t;

	icmp_hdr_t pckt;
	pckt.type = 8;          
	pckt.code = 0;          
	pckt.chksum = 0xfff7;   
	pckt.data = 0;

	struct sockaddr_in addr;
	addr.sin_family = AF_INET;
	addr.sin_port = 0;
	unsigned long i = 0;
	while (i <= range)
	{
		//ip
		ip_addr.s_addr = swapBytes(myNET + i);	//?
		addr.sin_addr.s_addr = inet_ntoa(ip_addr);
		bind(NewSocket, (SOCKADDR *)&addr, sizeof(addr));

		//Send
		int actionSendResult = sendto(NewSocket, &pckt, sizeof(pckt), 0, (struct sockaddr*)&addr, sizeof(addr));
		if (actionSendResult >= 0)
		{
			//Preparing for recieve
			unsigned int resAddressSize;
			unsigned char res[30] = "";
			struct sockaddr resAddress;


			//Answer
			int response = recvfrom(NewSocket, res, sizeof(res), 0, (SOCKADDR*)&resAddress, &resAddressSize);
			if (response < 0)
			{
				//Preparing for APR
				DWORD dwRetVal;
				IPAddr DestIp = myNET + i;
				IPAddr SrcIp = decIP;
				ULONG MacAddr[2];
				ULONG PhysAddrLen = 6;
				BYTE *bPhysAddr;	//for MAC

				//Send
				dwRetVal = SendARP(swapBytes(DestIp), swapBytes(SrcIp), &MacAddr, &PhysAddrLen);
				if (dwRetVal == NO_ERROR)
				{
					printf("---  %s ", inet_ntoa(ip_addr));
					//Print MAC
					bPhysAddr = (BYTE *)& MacAddr;
					if (PhysAddrLen) {
						for (int j = 0; j < (int)PhysAddrLen; j++) {
							if (j == (PhysAddrLen - 1))
								printf("%.2X ", (int)bPhysAddr[j]);
							else
								printf("%.2X-", (int)bPhysAddr[j]);
						}
					}

					//Preparing for getnameinfo
					struct sockaddr_in sa;

					sa.sin_family = AF_INET;
					sa.sin_port = 0;
					sa.sin_addr.s_addr = addr.sin_addr.s_addr;

					struct hostent *remoteHost;
					remoteHost = gethostbyaddr((char*)&ip_addr, 4, AF_INET);

					if (remoteHost != NULL)
						printf("%s\n", remoteHost->h_name);
					else
						printf("Cant Read Name(\n");
				}
			}
		}
		i++;
	}
}

void main()
{
	getMAC();
	getNetworkMAC();
	system("pause");
}