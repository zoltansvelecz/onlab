declare global {
    interface ReadableStream<R> {
        [Symbol.asyncIterator](): AsyncIterableIterator<R>;
    }
}

const BASE_URL = "http://localhost:5130/api/HR";

export default class HRService {

    private async postRequest(endpoint: string, cv: string): Promise<string> {
        try {
            const response = await fetch(`${BASE_URL}/${endpoint}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ cv })
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`HTTP ${response.status} - ${errorText}`);
            }

            const result = await response.text();

            return result;

        } catch (error: unknown) {
            if (error instanceof Error) {
                console.error("Hiba tortent:", error.message);
                return `Hiba tortent: ${error.message}`;
            } else {
                console.error("Ismeretlen hiba:", error);
                return "Ismeretlen hiba tortent.";
            }
        }
    }

    public async getCompetences(cv: string): Promise<string> {
        return this.postRequest("competences", cv);
    }

    public async getPositions(cv: string): Promise<string> {
        return this.postRequest("positions", cv);
    }

    public async getData(cv: string): Promise<string> {
        return this.postRequest("data", cv);
    }

    public async getQuestions(cv: string): Promise<string> {
        return this.postRequest("questions", cv);
    }

    public async getInvitation(cv: string): Promise<string> {
        return this.postRequest("invitation", cv);
    }

}