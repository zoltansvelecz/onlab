import HRService from "../services/HRService.ts";
import Module from "./Module.tsx";

export default function Interview(props: { cv: string }) {

    const service = new HRService();

    async function getQuestions(): Promise<string> {
        return await service.getQuestions(props.cv);
    }

    async function getInvitation(): Promise<string> {
        return await service.getInvitation(props.cv);
    }

    return (
        <div>
            <Module title="Invitation letter" getter={getInvitation}/>
            <Module title="Questions" getter={getQuestions}/>
        </div>
    );
}
