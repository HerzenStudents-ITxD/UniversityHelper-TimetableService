import axios from 'axios';
import type { Group, Subject, CreateGroupDto, UpdateGroupDto, CreateSubjectDto, UpdateSubjectDto } from '../types';

const api = axios.create({
  baseURL: 'http://localhost:5000/api',
});

// Groups
export const getAllGroups = async (): Promise<Group[]> => {
  const { data } = await api.get<Group[]>('/timetable/groups');
  return data;
};

export const getGroupById = async (id: string): Promise<Group> => {
  const { data } = await api.get<Group>(`/timetable/groups/${id}`);
  return data;
};

export const createGroup = async (group: CreateGroupDto): Promise<Group> => {
  const { data } = await api.post<Group>('/timetable/groups', group);
  return data;
};

export const updateGroup = async (id: string, group: UpdateGroupDto): Promise<Group> => {
  const { data } = await api.put<Group>(`/timetable/groups/${id}`, group);
  return data;
};

export const deleteGroup = async (id: string): Promise<void> => {
  await api.delete(`/timetable/groups/${id}`);
};

// Subjects
export const getSubjectById = async (id: string): Promise<Subject> => {
  const { data } = await api.get<Subject>(`/timetable/subjects/${id}`);
  return data;
};

export const getSubjectsByGroupId = async (groupId: string): Promise<Subject[]> => {
  const { data } = await api.get<Subject[]>(`/timetable/groups/${groupId}/subjects`);
  return data;
};

export const getSubjectsByPointId = async (pointId: string): Promise<Subject[]> => {
  const { data } = await api.get<Subject[]>(`/timetable/points/${pointId}/subjects`);
  return data;
};

export const createSubject = async (subject: CreateSubjectDto): Promise<Subject> => {
  const { data } = await api.post<Subject>('/timetable/subjects', subject);
  return data;
};

export const updateSubject = async (id: string, subject: UpdateSubjectDto): Promise<Subject> => {
  const { data } = await api.put<Subject>(`/timetable/subjects/${id}`, subject);
  return data;
};

export const deleteSubject = async (id: string): Promise<void> => {
  await api.delete(`/timetable/subjects/${id}`);
};

// Timetable
export const getTimetableForGroup = async (
  groupId: string,
  startDate: string,
  endDate: string
): Promise<Subject[]> => {
  const { data } = await api.get<Subject[]>(`/timetable/groups/${groupId}/timetable`, {
    params: { startDate, endDate },
  });
  return data;
};

export const getTimetableForPoint = async (
  pointId: string,
  startDate: string,
  endDate: string
): Promise<Subject[]> => {
  const { data } = await api.get<Subject[]>(`/timetable/points/${pointId}/timetable`, {
    params: { startDate, endDate },
  });
  return data;
}; 