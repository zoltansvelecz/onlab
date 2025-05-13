import HRService from "../services/HRService.ts";
import Module from "./Module.tsx";

export default function Overview(props: { cv: string }) {

    const service = new HRService();

    async function getCompetences(): Promise<string> {
        return await service.getCompetences(props.cv);
    }

    async function getPositions(): Promise<string> {
        return await service.getPositions(props.cv);
    }

    async function getData(): Promise<string> {
        return await service.getData(props.cv);
    }

    return (
        <div className="row">
            <div className="col-8">
                <Module title="Data" getter={getData}/>
                <Module title="Key competences" getter={getCompetences}/>
            </div>
            <div className="col-4">
                <Module title="Positions" getter={getPositions}/>
            </div>
        </div>
    );
}
