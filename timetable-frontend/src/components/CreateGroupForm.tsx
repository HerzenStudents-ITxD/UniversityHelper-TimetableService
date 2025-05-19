import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createGroup } from '../api';
import type { CreateGroupDto, Group } from '../types';

const createGroupSchema = z.object({
  institute: z.string().min(1, 'Institute is required'),
  faculcity: z.string().min(1, 'Faculty is required'),
  degree: z.string().min(1, 'Degree is required'),
  formEducation: z.string().min(1, 'Form of education is required'),
  course: z.number().min(1, 'Course must be at least 1'),
  group: z.string().min(1, 'Group is required'),
  direction: z.string().min(1, 'Direction is required'),
  subGroup: z.string().min(1, 'Subgroup is required'),
});

type CreateGroupFormData = z.infer<typeof createGroupSchema>;

export function CreateGroupForm() {
  const queryClient = useQueryClient();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateGroupFormData>({
    resolver: zodResolver(createGroupSchema),
  });

  const { mutate, isPending } = useMutation<Group, Error, CreateGroupDto>({
    mutationFn: createGroup,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['groups'] });
      reset();
    },
  });

  const onSubmit = (data: CreateGroupFormData) => {
    mutate(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6 max-w-2xl mx-auto p-6">
      <h2 className="text-2xl font-bold mb-6">Create New Group</h2>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div>
          <label className="block text-sm font-medium text-gray-700">Institute</label>
          <input
            type="text"
            {...register('institute')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.institute && (
            <p className="mt-1 text-sm text-red-600">{errors.institute.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Faculty</label>
          <input
            type="text"
            {...register('faculcity')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.faculcity && (
            <p className="mt-1 text-sm text-red-600">{errors.faculcity.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Degree</label>
          <input
            type="text"
            {...register('degree')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.degree && (
            <p className="mt-1 text-sm text-red-600">{errors.degree.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Form of Education</label>
          <input
            type="text"
            {...register('formEducation')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.formEducation && (
            <p className="mt-1 text-sm text-red-600">{errors.formEducation.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Course</label>
          <input
            type="number"
            {...register('course', { valueAsNumber: true })}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.course && (
            <p className="mt-1 text-sm text-red-600">{errors.course.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Group</label>
          <input
            type="text"
            {...register('group')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.group && (
            <p className="mt-1 text-sm text-red-600">{errors.group.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Direction</label>
          <input
            type="text"
            {...register('direction')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.direction && (
            <p className="mt-1 text-sm text-red-600">{errors.direction.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Subgroup</label>
          <input
            type="text"
            {...register('subGroup')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.subGroup && (
            <p className="mt-1 text-sm text-red-600">{errors.subGroup.message}</p>
          )}
        </div>
      </div>

      <div className="flex justify-end">
        <button
          type="submit"
          disabled={isPending}
          className="inline-flex justify-center rounded-md border border-transparent bg-indigo-600 py-2 px-4 text-sm font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 disabled:opacity-50"
        >
          {isPending ? 'Creating...' : 'Create Group'}
        </button>
      </div>
    </form>
  );
} 