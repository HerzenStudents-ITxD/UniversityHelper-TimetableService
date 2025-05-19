import axios from 'axios';

const API_URL = 'http://localhost:5000/api';

export interface Group {
  id: string;
  name: string;
}

export interface Subject {
  id: string;
  name: string;
  teacher: string;
  room: string;
  startTime: string;
  endTime: string;
  dayOfWeek: number;
}

export const timetableApi = {
  getGroups: async (): Promise<Group[]> => {
    const response = await axios.get(`${API_URL}/groups`);
    return response.data;
  },

  getTimetable: async (groupId: string): Promise<Subject[]> => {
    const response = await axios.get(`${API_URL}/groups/${groupId}/timetable`);
    return response.data;
  },
}; 