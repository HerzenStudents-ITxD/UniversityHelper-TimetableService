import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createSubject } from '../api';
import type { CreateSubjectDto, Subject } from '../types';

const createSubjectSchema = z.object({
  groupId: z.string().min(1, 'Group is required'),
  name: z.string().min(1, 'Subject name is required'),
  date: z.string().min(1, 'Date is required'),
  professor: z.string().min(1, 'Professor is required'),
  pointId: z.string().optional(),
  place: z.string().min(1, 'Place is required'),
});

type CreateSubjectFormData = z.infer<typeof createSubjectSchema>;

interface CreateSubjectFormProps {
  groupId: string;
}

export function CreateSubjectForm({ groupId }: CreateSubjectFormProps) {
  const queryClient = useQueryClient();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateSubjectFormData>({
    resolver: zodResolver(createSubjectSchema),
    defaultValues: {
      groupId,
    },
  });

  const { mutate, isPending } = useMutation<Subject, Error, CreateSubjectDto>({
    mutationFn: createSubject,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['subjects', groupId] });
      reset();
    },
  });

  const onSubmit = (data: CreateSubjectFormData) => {
    mutate(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6 max-w-2xl mx-auto p-6">
      <h2 className="text-2xl font-bold mb-6">Add New Subject</h2>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div>
          <label className="block text-sm font-medium text-gray-700">Subject Name</label>
          <input
            type="text"
            {...register('name')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.name && (
            <p className="mt-1 text-sm text-red-600">{errors.name.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Date and Time</label>
          <input
            type="datetime-local"
            {...register('date')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.date && (
            <p className="mt-1 text-sm text-red-600">{errors.date.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Professor</label>
          <input
            type="text"
            {...register('professor')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.professor && (
            <p className="mt-1 text-sm text-red-600">{errors.professor.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Place</label>
          <input
            type="text"
            {...register('place')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.place && (
            <p className="mt-1 text-sm text-red-600">{errors.place.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">Point ID (Optional)</label>
          <input
            type="text"
            {...register('pointId')}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
          {errors.pointId && (
            <p className="mt-1 text-sm text-red-600">{errors.pointId.message}</p>
          )}
        </div>
      </div>

      <div className="flex justify-end">
        <button
          type="submit"
          disabled={isPending}
          className="inline-flex justify-center rounded-md border border-transparent bg-indigo-600 py-2 px-4 text-sm font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 disabled:opacity-50"
        >
          {isPending ? 'Creating...' : 'Add Subject'}
        </button>
      </div>
    </form>
  );
} 