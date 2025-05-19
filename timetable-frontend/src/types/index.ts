export interface Group {
  id: string;
  institute: string;
  faculcity: string;
  degree: string;
  formEducation: string;
  course: number;
  group: string;
  direction: string;
  subGroup: string;
  subjects: Subject[];
}

export interface Subject {
  id: string;
  groupId: string;
  name: string;
  date: string;
  professor: string;
  pointId?: string;
  place: string;
}

export interface CreateGroupDto {
  institute: string;
  faculcity: string;
  degree: string;
  formEducation: string;
  course: number;
  group: string;
  direction: string;
  subGroup: string;
}

export interface UpdateGroupDto extends CreateGroupDto {}

export interface CreateSubjectDto {
  groupId: string;
  name: string;
  date: string;
  professor: string;
  pointId?: string;
  place: string;
}

export interface UpdateSubjectDto {
  name: string;
  date: string;
  professor: string;
  pointId?: string;
  place: string;
} 