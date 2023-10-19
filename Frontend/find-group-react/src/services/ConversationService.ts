import {AxiosInstance} from "axios";
import {IConversation} from "../domain/IConversation";
import {IAddMessage} from "../dto/IAddMessage";

export class ConversationService {
    private groupUrl = 'v1/Conversation/';
    private axios: AxiosInstance;

    constructor(private axiosInstance: AxiosInstance) {
        this.axios = axiosInstance;
    }

    async getGroupConversation(groupId: string): Promise<IConversation | undefined> {
        try {
            const response = await this.axios.get<IConversation>(this.groupUrl + groupId);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return undefined;
    }

    async addMessage(addMessage: IAddMessage): Promise<void> {
        try {
            const response = await this.axios.post(this.groupUrl, addMessage);
            if (response.status === 200) {
                return;
            }
        } catch (e) {
            console.log(e);
        }
        return;
    }
}